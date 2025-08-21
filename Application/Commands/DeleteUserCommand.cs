using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.Commands;

/// <summary>
/// Command for deleting an existing user.
/// </summary>
public class DeleteUserCommand : IRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Invalid user ID.")]
    public int Id { get; set; }
}
