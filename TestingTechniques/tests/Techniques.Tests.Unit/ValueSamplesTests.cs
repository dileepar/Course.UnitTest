using FluentAssertions;
using TestingTechniques;

namespace Techniques.Tests.Unit;

public class ValueSamplesTests
{
    private readonly ValueSamples _sut = new();
    
    //Testing string properties
    [Fact]
    public void FullName_ShouldBeNickChapsas_WhenValueSamplesIsCreated()
    {
        // Arrange
        var expected = "Nick Chapsas";
        
        // Act
        var result = _sut.FullName;
        
        // Assert
        result.Should().Be(expected);
        result.Should().StartWith("Nick");
        result.Should().EndWith("Chapsas");
    }
    
    //Testing integer properties
    [Fact]
    public void Age_ShouldBe21_WhenValueSamplesIsCreated()
    {
        // Arrange
        var expected = 21;
        
        // Act
        var result = _sut.Age;
        
        // Assert
        result.Should().Be(expected);
        result.Should().BePositive();
        result.Should().BeInRange(18, 60);
    }
    
    //Testing DateOnly properties
    [Fact]
    public void DateOfBirth_ShouldBe2000June9_WhenValueSamplesIsCreated()
    {
        // Arrange
        var expected = new DateOnly(2000, 6, 9);
        
        // Act
        var result = _sut.DateOfBirth;
        
        // Assert
        result.Should().Be(expected);
        result.Should().BeAfter(DateOnly.MinValue);
        result.Should().BeBefore(DateOnly.MaxValue);
    }
    
    //Testing object properties
    [Fact]
    public void AppUser_ShouldBeNickChapsas_WhenValueSamplesIsCreated()
    {
        // Arrange
        var expected = new User()
        {
            FullName = "Nick Chapsas",
            Age = 21,
            DateOfBirth = new DateOnly(2000, 6, 9)
        };
        
        // Act
        var result = _sut.AppUser;
        
        // Assert
        result.Should().BeEquivalentTo(expected);
        result.Should().BeOfType<User>();
    }
    
    //Testing IEnumerable numbers
    [Fact]
    public void Numbers_ShouldContain1_5_10_15_WhenValueSamplesIsCreated()
    {
        // Arrange
        var expected = _sut.Numbers.As<int[]>();
        
        // Act
        var result = _sut.Numbers;
        
        // Assert
        result.Should().BeEquivalentTo(expected);
        result.Should().HaveCount(4);
        result.Should().Contain(5);
        result.Should().NotContain(20);
    }
    
    //Testing event
    [Fact]
    public void Should_UserInUsers_WhenValueSamplesIsCreated()
    {
        var user = new User
        {
            FullName = "Nick Chapsas",
            Age = 21,
            DateOfBirth = new DateOnly(2000, 6, 9)
        };
        
        var users = _sut.Users.As<User[]>();

        users.Should().ContainEquivalentOf(user);
        users.Should().HaveCount(3);
        users.Should().Contain(x=>x.FullName.StartsWith("Nick") && x.Age > 5);
    }
    
    //Testing internal members
    [Fact]
    public void TestInternalmember()
    {
        var result = _sut.AnInternelMethod();
        
        result.Should().Be("I am internal!");
    }
}