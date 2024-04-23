using FluentAssertions;
using Microsoft.Data.Sqlite;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Users.Api.Logging;
using Users.Api.Models;
using Users.Api.Repositories;
using Users.Api.Services;

namespace Users.Api.Tests.Unit;

public class UserServiceTests
{
    private readonly UserService _sut;
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly ILoggerAdapter<UserService> _logger = Substitute.For<ILoggerAdapter<UserService>>();
    
    public UserServiceTests()
    {
        _sut = new UserService(_userRepository, _logger);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        _userRepository.GetAllAsync().Returns(Enumerable.Empty<User>());

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }
    
    [Fact]
    public async Task GetAllAsync_ShouldReturnUsers_WhenSomeUsersExist()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = "John Doe"
        };
        var users = new[]
        {
            user
        };
        _userRepository.GetAllAsync().Returns(users);
        
        // Act
        var result = await _sut.GetAllAsync();
        
        // Assert
        result.Should().ContainSingle(i => i.FullName == "John Doe");
        result.Single().Should().BeEquivalentTo(user);
    }
    
    //We created ILoggerAdapter<T> to be able to test the logging as we can't
    //directly test the ILogger<T> interface. 
    //ILoggerAdapter is a wrapper around ILogger<T> and we can test it.
    [Fact]
    public async Task GetAllAsync_ShouldLogInformation_WhenCalled()
    {
        //Act
        await _sut.GetAllAsync();
        
        //Assert
        //_logger.Received(1).LogInformation(Arg.Is("Retrieving all users"));
        _logger.Received(1).LogInformation(Arg.Is<string>(i=>i.StartsWith("Retrieving")));
        _logger.Received(1).LogInformation(Arg.Is("All users retrieved in {0}ms"), Arg.Any<long>());
    }

    [Fact]
    public async Task GetAllAsync_ShouldLogMessageAndException_WhenExceptionThrown()
    {
        //Arrange
        _userRepository.GetAllAsync().Throws(new SqliteException("An error occurred",500));
        
        //Act
        var requestAction = async () => await _sut.GetAllAsync();
        
        //Assert
        await requestAction.Should().ThrowAsync<SqliteException>()
            .WithMessage("An error occurred");
        _logger.Received(1).LogError(Arg.Any<SqliteException>(), 
            Arg.Is("Something went wrong while retrieving all users"));
    }
}