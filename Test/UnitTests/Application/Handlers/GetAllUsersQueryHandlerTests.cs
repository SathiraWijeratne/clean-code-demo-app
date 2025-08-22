using System;
using Application.Handlers;
using Application.Queries;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Test.UnitTests;

/// <summary>
/// Unit tests for the GetAllUsersQueryHandler class.
/// </summary>
public class GetAllUsersQueryHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly GetAllUsersQueryHandler _handler;

    public GetAllUsersQueryHandlerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _handler = new GetAllUsersQueryHandler(_mockUserRepository.Object);
    }

    [Fact]
    public async Task Handle_WhenUsersExist_ShouldReturnUserDtos()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Name = "John Doe" },
            new User { Id = 2, Name = "Jane Smith" }
        };

        _mockUserRepository.Setup(x => x.GetAllUsersAsync())
                          .ReturnsAsync(users);

        var query = new GetAllUsersQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        var userDtos = result.ToList();
        Assert.Equal(2, userDtos.Count);
        Assert.Equal("John Doe", userDtos[0].Name);
        Assert.Equal("Jane Smith", userDtos[1].Name);
    }

    [Fact]
    public async Task Handle_WhenNoUsersExist_ShouldReturnEmptyCollection()
    {
        // Arrange
        _mockUserRepository.Setup(x => x.GetAllUsersAsync())
                          .ReturnsAsync(new List<User>());

        var query = new GetAllUsersQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
