using System;
using Application.Commands;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers;

/// <summary>
/// Handler for updating an existing user.
/// </summary>
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = request.Id,
            Name = request.Name
        };

        await _userRepository.UpdateUserAsync(user);
    }
}
