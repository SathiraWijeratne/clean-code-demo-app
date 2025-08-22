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

    [Fact]
    public async Task Handle_ValidQuery_ShouldCallRepositoryWithCorrectId()
    {
        var userId = 42;
        var query = new GetUserByIdQuery { Id = userId };
        var user = new User { Id = userId, Name = "Test User" };

        _mockUserRepository.Setup(x => x.GetUserByIdAsync(userId))
            .ReturnsAsync(user);

        await _handler.Handle(query, CancellationToken.None);

        _mockUserRepository.Verify(x => x.GetUserByIdAsync(userId), Times.Once);
    }

    [Fact]
    public async Task Handle_UserWithDifferentProperties_ShouldMapCorrectly()
    {
        var userId = 123;
        var userName = "Jane Smith";
        var user = new User { Id = userId, Name = userName };
        var query = new GetUserByIdQuery { Id = userId };

        _mockUserRepository.Setup(x => x.GetUserByIdAsync(userId))
            .ReturnsAsync(user);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<UserDto>(result);
        Assert.Equal(userId, result.Id);
        Assert.Equal(userName, result.Name);
    }

    [Fact]
    public async Task Handle_RepositoryThrowsException_ShouldPropagateException()
    {
        var userId = 1;
        var query = new GetUserByIdQuery { Id = userId };
        var expectedException = new InvalidOperationException("Database error");

        _mockUserRepository.Setup(x => x.GetUserByIdAsync(userId))
            .ThrowsAsync(expectedException);

        var actualException = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _handler.Handle(query, CancellationToken.None));

        Assert.Equal(expectedException.Message, actualException.Message);
        _mockUserRepository.Verify(x => x.GetUserByIdAsync(userId), Times.Once);
    }

    [Fact]
    public async Task Handle_CancellationRequested_ShouldStillCallRepository()
    {
        var userId = 1;
        var query = new GetUserByIdQuery { Id = userId };
        var user = new User { Id = userId, Name = "Test User" };
        var cancellationToken = new CancellationToken(true);

        _mockUserRepository.Setup(x => x.GetUserByIdAsync(userId))
            .ReturnsAsync(user);

        var result = await _handler.Handle(query, cancellationToken);

        Assert.NotNull(result);
        _mockUserRepository.Verify(x => x.GetUserByIdAsync(userId), Times.Once);
    }
}
