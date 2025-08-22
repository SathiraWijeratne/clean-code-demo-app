using System;
using Application.Commands;
using Application.Handlers;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Test.UnitTests.Application.Handlers;

/// <summary>
/// Unit tests for the CreateUserCommandHandler class.
/// </summary>
public class CreateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _handler = new CreateUserCommandHandler(_mockUserRepository.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCallAddUserAsync()
    {
        var command = new CreateUserCommand{Name = "John Doe"};
        _mockUserRepository.Setup(x => x.AddUserAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        _mockUserRepository.Verify(x=> x.AddUserAsync(It.Is<User>(u => u.Name == "John Doe")), Times.Once);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateUserWithCorrectName()
    {
        var command = new CreateUserCommand { Name = "Jane Smith" };
        User capturedUser = null!;
        
        _mockUserRepository.Setup(x => x.AddUserAsync(It.IsAny<User>()))
            .Callback<User>(user => capturedUser = user)
            .Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(capturedUser);
        Assert.Equal("Jane Smith", capturedUser.Name);
        Assert.Equal(0, capturedUser.Id);
    }
}
