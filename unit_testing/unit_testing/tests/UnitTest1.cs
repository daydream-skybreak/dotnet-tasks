using Xunit.Abstractions;
using unit_testing.functions;

namespace unit_testing.tests;

public class UnitTest1
{
    private readonly ArithmaticOperations _arithmatic = new ArithmaticOperations();

    [Fact]
    public void TestAdd()
    {
        int result = _arithmatic.Add(5, 3);
        Assert.Equal(8, result);
    }

    [Fact]
    public void TestSubtract()
    {
        int result = _arithmatic.Subtract(5, 3);
        Assert.Equal(2, result);
    }

    [Fact]
    public void TestMultiply()
    {
        int result = _arithmatic.Multiply(5, 3);
        Assert.Equal(15, result);
    }

    [Fact]
    public void TestDivide()
    {
        int result = _arithmatic.Divide(5, 3);
        Assert.Equal(1, result);
    }
}