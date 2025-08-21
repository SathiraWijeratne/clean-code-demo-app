using System;
using Application.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

/// <summary>
/// Query for retrieving all users.
/// </summary>
public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
{

}
