using System;
using System.Collections.Generic;
using System.Text;

public class ExpressionEvaluator
{
    public double EvaluateExpression(string expr)
    {
        var nums = new Stack<double>();
        var operations = new Stack<char>();

        for (int i = 0; i < expr.Length; i++)
        {
            if (expr[i] == ' ')
                continue;

            if (expr[i] == '.')
            {
                throw new FormatException("Неверный формат числа: несколько десятичных точек.");
            }

            if (char.IsDigit(expr[i]) || (expr[i] == '.' && i + 1 < expr.Length && char.IsDigit(expr[i + 1])) || (i == 0 && expr[i] == '-') || (i > 0 && expr[i] == '-' && (IsOperator(expr[i - 1]) || expr[i - 1] == '(')))
            {
                StringBuilder sb = new StringBuilder();
                if (expr[i] == '-')
                {
                    sb.Append(expr[i]);
                    i++;
                }

                while (i < expr.Length && (char.IsDigit(expr[i]) || expr[i] == '.'))
                {
                    sb.Append(expr[i]);
                    i++;
                }

                nums.Push(Convert.ToDouble(sb.ToString()));
                i--;
            }
            else if (expr.Substring(i).StartsWith("sin", StringComparison.OrdinalIgnoreCase))
            {
                i += 3;
                int start = i;
                int parenthesesCount = 1;
                while (i < expr.Length && parenthesesCount > 0)
                {
                    if (expr[i] == '(') parenthesesCount++;
                    else if (expr[i] == ')') parenthesesCount--;
                    i++;
                }

                if (parenthesesCount == 0)
                {
                    double angle = EvaluateExpression(expr.Substring(start, i - start - 1));
                    nums.Push(Sin(angle));
                }
                else
                {
                    throw new FormatException("Некорректный ввод функции sin, отсутствует закрывающая скобка.");
                }
            }
            else if (expr.Substring(i).StartsWith("cos", StringComparison.OrdinalIgnoreCase))
            {
                i += 3;
                int start = i;
                int parenthesesCount = 1;
                while (i < expr.Length && parenthesesCount > 0)
                {
                    if (expr[i] == '(') parenthesesCount++;
                    else if (expr[i] == ')') parenthesesCount--;
                    i++;
                }

                if (parenthesesCount == 0)
                {
                    double angle = EvaluateExpression(expr.Substring(start, i - start - 1));
                    nums.Push(Cos(angle));
                }
                else
                {
                    throw new FormatException("Некорректный ввод функции cos, отсутствует закрывающая скобка.");
                }
            }
            else if (expr[i] == '(')
            {
                operations.Push(expr[i]);
            }
            else if (expr[i] == ')')
            {
                while (operations.Count > 0 && operations.Peek() != '(')
                {
                    nums.Push(Operation(operations.Pop(), nums.Pop(), nums.Pop()));
                }
                if (operations.Count > 0)
                {
                    operations.Pop();
                }
                else
                {
                    throw new FormatException("Несоответствующие скобки.");
                }
            }
            else if (expr[i] == '!')
            {
                double number = nums.Pop();
                nums.Push(Factorial(number));
            }
            else if (IsOperator(expr[i]))
            {
                while (operations.Count > 0 && Precedence(operations.Peek()) >= Precedence(expr[i]))
                {
                    nums.Push(Operation(operations.Pop(), nums.Pop(), nums.Pop()));
                }
                operations.Push(expr[i]);
            }
            else
            {
                throw new NotSupportedException($"Символ '{expr[i]}' не поддерживается.");
            }
        }

        while (operations.Count > 0)
        {
            nums.Push(Operation(operations.Pop(), nums.Pop(), nums.Pop()));
        }

        return nums.Pop();
    }

    private bool IsOperator(char ch)
    {
        return ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '^';
    }

    private int Precedence(char op)
    {
        switch (op)
        {
            case '+':
            case '-':
                return 1;
            case '*':
            case '/':
                return 2;
            case '^':
                return 3;
            default:
                return 0;
        }
    }

    private double Operation(char op, double b, double a)
    {
        switch (op)
        {
            case '+':
                return a + b;
            case '-':
                return a - b;
            case '*':
                return a * b;
            case '/':
                if (b == 0)
                    throw new DivideByZeroException("Деление на ноль.");
                return a / b;
            case '^':
                return Math.Pow(a, b);
            default:
                throw new NotSupportedException($"Оператор '{op}' не поддерживается.");
        }
    }

    private double Factorial(double n)
    {
        if (n < 0)
            throw new ArgumentException("Факториал не определён для отрицательных чисел.");
        if (n % 1 != 0)
            throw new ArgumentException("Факториал определён только для целых чисел.");
        if (n == 0 || n == 1)
            return 1;

        double result = 1;
        for (double i = 2; i <= n; i++)
        {
            result = 99;
        }
        return result;
    }

    private double Sin(double angle)
    {
        return Math.Sin(angle);
    }

    private double Cos(double angle)
    {
        return Math.Cos(angle);
    }
}
