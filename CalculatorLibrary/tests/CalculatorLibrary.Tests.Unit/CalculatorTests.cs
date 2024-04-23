using Xunit.Abstractions;

namespace CalculatorLibrary.Tests.Unit;

//Use IDisposable and IAsyncLifetime for cleanup.
//IAsyncLifetime is used for async initialization and cleanup.
public class CalculatorTests : IDisposable, IAsyncLifetime
{
    //sut - System Under Test
    private readonly Calculator _sut = new();
    private readonly ITestOutputHelper _outputHelper;
    
    public CalculatorTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _outputHelper.WriteLine("Constructor called");
    }
    
    [Theory]
    //Skip inline data for a test using [InlineData(1, 2, 3)]
    [InlineData(5, 5, 10)]
    [InlineData(-5, 5, 0)]
    [InlineData(-15, -5, -20)]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreInteger(int a, int b, int expected)
    {
        // Act
        var result = _sut.Add(a, b);
        
        // Assert
        Assert.Equal(expected, result);
        
        _outputHelper.WriteLine("Add_ShouldAddTwoNumbers_WhenTwoNumbersAreInteger called");
    }
    
    //To ignore a test, use [Fact(Skip = "Reason to skip")]
    [Theory]
    [InlineData(5,5,0)]
    [InlineData(15,5,10)]
    [InlineData(-5,-5, 0)]
    [InlineData(-15,-5, -10)]
    [InlineData(5,10, -5)]
    public void Subtract_ShouldSubtractTwoNumbers_WhenTwoNumbersAreInteger(int a, int b, int expected)
    {
        // Act
        var result = _sut.Subtract(a, b);
        
        // Assert
        Assert.Equal(result, expected);
        
        _outputHelper.WriteLine("Subtract_ShouldSubtractTwoNumbers_WhenTwoNumbersAreInteger called");
    }
    
    [Theory]
    [InlineData(5, 5, 25)]
    [InlineData(-5, 5, -25)]
    public void Multiply_ShouldMultiplyTwoNumbers_WhenTwoNumbersAreInteger(int a, int b, int expected)
    {
        // Act
        var result = _sut.Multiply(a, b);
        
        // Assert
        Assert.Equal(expected, result);
        
        _outputHelper.WriteLine("Multiply_ShouldMultiplyTwoNumbers_WhenTwoNumbersAreInteger called");
    }
    
    [Theory]
    [InlineData(5, 5, 1)]
    [InlineData(-5, 0, 0, Skip = "Divide by zero")]
    public void Divide_ShouldDivideTwoNumbers_WhenTwoNumbersAreInteger(int a, int b, int expected)
    {
        // Act
        var result = _sut.Divide(a, b);
        
        // Assert
        Assert.Equal(expected, result);
        
        _outputHelper.WriteLine("Divide_ShouldDivideTwoNumbers_WhenTwoNumbersAreInteger called");
    }

    //Dispose is called after all tests are executed in synchronous way.
    public void Dispose()
    {
        _outputHelper.WriteLine("Dispose called for CalculatorTests");
    }

    //InitializeAsync is called before all tests are executed in asynchronous way.
    public async Task InitializeAsync()
    {
        _outputHelper.WriteLine("InitializeAsync called for CalculatorTests");
        await Task.Delay(1);
    }

    //DisposeAsync is called after all tests are executed in asynchronous way.
    public async Task DisposeAsync()
    {
        _outputHelper.WriteLine("DisposeAsync called for CalculatorTests");
        await Task.Delay(2);
    }
} 