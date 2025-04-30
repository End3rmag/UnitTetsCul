using NUnit.Framework;

namespace ConsoleApp1;

public class NUnitTestItem1
{
    private ExpressionEvaluator cl;
    [SetUp]
    public void Setup()
    {
       cl = new ExpressionEvaluator();
    }


    [Test]
    public void TestAddition()
    {
        double result = cl.EvaluateExpression("3 + 5");
        Assert.AreEqual(8, result);
    }

    [Test]
    public void TestSubtraction()
    {
        double result = cl.EvaluateExpression("10 - 2");
        Assert.AreEqual(8, result);
    }

    [Test]
    public void TestMultiplication()
    {
        double result = cl.EvaluateExpression("4 * 5");
        Assert.AreEqual(20, result); 
    }

    [Test]
    public void TestDivision()
    {
        double result = cl.EvaluateExpression("16 / 2");
        Assert.AreEqual(8, result);
    }

    [Test]
    public void TestDivisionByZero()
    {
        Assert.Throws<DivideByZeroException>(() => cl.EvaluateExpression("1 / 0"));
    }

    [Test]
    public void TestSinFunction()
    {
        Assert.Throws<FormatException>(() => cl.EvaluateExpression("sin(0)"));
    }

    [Test]
    public void TestCosFunction()
    {
        Assert.Throws<FormatException>(() => cl.EvaluateExpression("cos(0)"));
    }

    [Test]
    public void TestFactorial()
    {
        double result = cl.EvaluateExpression("3!");
        Assert.AreEqual(99, result);
    }

    [Test]
    public void TestInvalidOperation()
    {
        Assert.Throws<NotSupportedException>(() => cl.EvaluateExpression("3 $ 4"));
    }

    [Test]
    public void TestInvalidFormat()
    {
        Assert.Throws<FormatException>(() => cl.EvaluateExpression("10..5"));
    }

    [Test]
    public void ExpressionWithMultipleOperators()
    {
        string expression = "1 + 2 * 3 - 4 / 2";
        double result = cl.EvaluateExpression(expression);
        Assert.AreEqual(5, result);
    }

    [Test]
    public void ComplexExpressionWithBrackets()
    {
        string expression = "(1 + 2) * (3 - 4)";
        double result = cl.EvaluateExpression(expression);
        Assert.AreEqual(-3, result);
    }

    [Test]
    public void SinOfPiOver2()
    {
        Assert.Throws<FormatException>(() => cl.EvaluateExpression("sin(3.14159265359 / 2)"));
    }

    [Test]
    public void CosOfPi()
    {
        Assert.Throws<FormatException>(() => cl.EvaluateExpression("cos(3.14159265359)"));
    }

    [Test]
    public void NestedFunctionCalls()
    {
        Assert.Throws<FormatException>(() => cl.EvaluateExpression("sin(cos(0))"));
    }

    [Test]
    public void VeryLargeNumbers()
    {
        string expression = "999999999 + 1";
        double result = cl.EvaluateExpression(expression);
        Assert.AreEqual(1000000000, result);
    }

    [Test]
    public void RoundingInExpression()
    {
        Assert.Throws<FormatException>(() => cl.EvaluateExpression("2.4 * 2.5 - 3.6"));
    }

    [Test]
    public void ComplexNestedExpression()
    {
        string expression = "2 * (3 + 5) - 6 / (2 - 1)";
        double result = cl.EvaluateExpression(expression);
        Assert.AreEqual(10, result); 
    }

    [Test]
    public void OperatorsInDifferentOrder()
    {
        string expression = "6 / 2 * 3 + 4 - 5";
        double result = cl.EvaluateExpression(expression);
        Assert.AreEqual(8, result);
    }

    [Test]
    public void ExpressionWithDecimalNumbers()
    {
        
        Assert.Throws<FormatException>(() => cl.EvaluateExpression("3.5 + 2.2 - 1.5"));
    }

    [Test]
    public void ExpressionWithNegativeNumbers()
    {
        string expression = "-5 + 3";
        double result = cl.EvaluateExpression(expression);
        Assert.AreEqual(-2, result);
    }

    [Test]
    public void ExpressionWithParenthesesPrioritizing()
    {
        string expression = "1 + 2 * (3 + 4)";
        double result = cl.EvaluateExpression(expression);
        Assert.AreEqual(15, result);
    }

    [Test]
    public void ExpressionWithInvalidCharacters()
    {
        string expression = "5 + x";
        Assert.Throws<NotSupportedException>(() => cl.EvaluateExpression(expression));
    }

    [Test]
    public void ExpressionWithDecimalFactorial()
    {
        string expression = "4.5!";
        Assert.Throws<FormatException>(() => cl.EvaluateExpression(expression));
    }

    [Test]
    public void ExpressionWithFractionalNumbers()
    {
        string expression = "3.5 + 2.5";
        Assert.Throws<FormatException>(() => cl.EvaluateExpression(expression));
    }
    [Test]
    public void ExpressionWithNonCorrectFractionalNumberst()
    {
        string expression = "3,5 + 2,5";
        Assert.Throws<NotSupportedException>(() => cl.EvaluateExpression(expression));
    }

    [Test]
    public void ExpressionWithString()
    {
        string expression = "DASF";
        Assert.Throws<NotSupportedException>(() => cl.EvaluateExpression(expression));
    }

    [Test]
    public void ExtraOperator()
    {
        Assert.Throws(typeof(InvalidOperationException), () => cl.EvaluateExpression("2++3"));
    }

    [Test]
    public void EmptyBrackets()
    {
        Assert.Throws(typeof(InvalidOperationException), () => cl.EvaluateExpression("()"));
    }

    [Test]
    public void OnlyOperator()
    {
        Assert.Throws(typeof(InvalidOperationException), () => cl.EvaluateExpression("+"));
}


    }