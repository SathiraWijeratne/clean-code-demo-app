using System;
using Application.Dtos;
using Application.Handlers;
using Application.Queries;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Test.UnitTests.Application.Handlers;

/// <summary>
/// Unit tests for the GetUserByIdQueryHandler class.
/// </summary>
public class GetUserByIdQueryHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly GetUserByIdQueryHandler _handler;

    public GetUserByIdQueryHandlerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _handler = new GetUserByIdQueryHandler(_mockUserRepository.Object);
    }

    // Test for handling valid get user by id queries
    [Fact]
    public async Task Handle_UserExists_ShouldReturnUserDto()
    {
        var userId = 1;
        var userName = "John Doe";
        var user = new User { Id = userId, Name = userName };
        var query = new GetUserByIdQuery { Id = userId };

        _mockUserRepository.Setup(x => x.GetUserByIdAsync(userId))
            .ReturnsAsync(user);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
        Assert.Equal(userName, result.Name);
        _mockUserRepository.Verify(x => x.GetUserByIdAsync(userId), Times.Once);
    }

    // Test for handling user not found scenarios
    [Fact]
    public async Task Handle_UserDoesNotExist_ShouldReturnNull()
    {
        var userId = 999;
        var query = new GetUserByIdQuery { Id = userId };

        _mockUserRepository.Setup(x => x.GetUserByIdAsync(userId))
            .ReturnsAsync((User?)null);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.Null(result);
        _mockUserRepository.Verify(x => x.GetUserByIdAsync(userId), Times.Once);
    }
}
