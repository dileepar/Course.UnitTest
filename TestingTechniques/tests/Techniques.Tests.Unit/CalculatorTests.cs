using FluentAssertions;
using TestingTechniques;

namespace Techniques.Tests.Unit;

public class CalculatorTests
{
    private readonly Calculator _sut = new();

    [Fact]
    public void Should_Exception_When_DivideByZero()
    {
        // Arrange
        var a = 10;
        var b = 0;

        // Act
        Action result = () => _sut.Divide(a, b);

        // Assert
        result.Should().Throw<DivideByZeroException>();
    }
} 