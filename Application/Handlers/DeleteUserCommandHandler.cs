using System;
using Application.Commands;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers;

/// <summary>
/// Handler for deleting an existing user.
/// </summary>
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteUserAsync(request.Id);
    }
}
