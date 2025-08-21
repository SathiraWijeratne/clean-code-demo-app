using System;
using Application.Commands;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers;

/// <summary>
/// Handler for creating a new user.
/// </summary> 
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Name = request.Name
        };

        await _userRepository.AddUserAsync(user);
    }
}
