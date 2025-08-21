using System;
using Application.Dtos;
using Application.Queries;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers;

/// <summary>
/// Handler for retrieving all users.
/// </summary>
public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsersAsync();
        
        return users.Select(user => new UserDto
        {
            Id = user.Id,
            Name = user.Name
        });
    }
}
