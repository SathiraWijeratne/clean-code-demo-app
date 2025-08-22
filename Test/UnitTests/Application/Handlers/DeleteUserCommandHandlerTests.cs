using System;
using Application.Commands;
using Application.Handlers;
using Domain.Repositories;
using Moq;

namespace Test.UnitTests.Application.Handlers;

/// <summary>
/// Unit tests for the DeleteUserCommandHandler class.
/// </summary>
public class DeleteUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly DeleteUserCommandHandler _handler;

    public DeleteUserCommandHandlerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _handler = new DeleteUserCommandHandler(_mockUserRepository.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCallDeleteUserAsync()
    {       
        var command = new DeleteUserCommand { Id = 1 };
        _mockUserRepository.Setup(x => x.DeleteUserAsync(It.IsAny<int>()))
            .Returns(Task.CompletedTask);
    
        await _handler.Handle(command, CancellationToken.None);
    
        _mockUserRepository.Verify(x => x.DeleteUserAsync(1), Times.Once);
    }
}
