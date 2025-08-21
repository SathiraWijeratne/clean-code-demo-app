using System;
using System.ComponentModel.DataAnnotations;
using Application.Dtos;
using MediatR;

namespace Application.Queries;

/// <summary>
/// Query for retrieving a user by their ID.
/// </summary>
public class GetUserByIdQuery : IRequest<UserDto?>
{
    [Range(1, int.MaxValue, ErrorMessage = "Id must be a positive integer.")]
    public int Id { get; set; }
}
