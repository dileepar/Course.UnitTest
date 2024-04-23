using FluentAssertions;
using NSubstitute;
using UnderstandingDependencies.Api.Models;
using UnderstandingDependencies.Api.Repositories;
using UnderstandingDependencies.Api.Services;

namespace UnderstandingDependencies.Api.Tests.Unit;

public class UserServiceTests
{
  private readonly UserService _sut;
  private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();

  public UserServiceTests()
  {
    _sut = new UserService(_userRepository);
  }
  
  [Fact]
  public async void GetAllAsync_ShouldReturnEmptyList_WhenNoUsers()
  {
    // Arrange
    _userRepository.GetAllAsync().Returns(Array.Empty<User>());
    
    // Act
    var result = await _sut.GetAllAsync();
    
    // Assert
    result.Should().BeEmpty();
  }
  
  [Fact]
  public async void GetAllAsync_ShouldReturnUsers_WhenUsersExist()
  {
    // Arrange
    var users = new[]
    {
      new User { Id = Guid.NewGuid(), FullName = "John Doe" }
    };
    _userRepository.GetAllAsync().Returns(users);
    
    // Act
    var result = await _sut.GetAllAsync();
    
    // Assert
    result.Should().ContainSingle(i => i.FullName == "John Doe");
  }
}