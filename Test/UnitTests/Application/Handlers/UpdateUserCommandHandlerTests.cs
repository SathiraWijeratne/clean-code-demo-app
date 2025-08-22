using System;
using Application.Commands;
using Application.Handlers;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Test.UnitTests.Application.Handlers;

/// <summary>
/// Unit tests for the UpdateUserCommandHandler class.
/// </summary>
public class UpdateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly UpdateUserCommandHandler _handler;

    public UpdateUserCommandHandlerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _handler = new UpdateUserCommandHandler(_mockUserRepository.Object);
    }

    // Test for handling valid update commands
    [Fact]
    public async Task Handle_ValidCommand_ShouldCallUpdateUserAsync()
    {
        var command = new UpdateUserCommand { Id = 1, Name = "Updated Name" };
        _mockUserRepository.Setup(x => x.UpdateUserAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        _mockUserRepository.Verify(x => x.UpdateUserAsync(It.Is<User>(u => u.Id == 1 && u.Name == "Updated Name")), Times.Once);
    }
}
