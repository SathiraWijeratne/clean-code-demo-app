using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.Commands;

/// <summary>
/// Command for creating a new user.
/// </summary>
public class CreateUserCommand : IRequest
{
    [Required]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters.")]
    [RegularExpression(@"^[a-zA-Z]+(?:\s[a-zA-Z]+)*$", ErrorMessage = "Name must contain only letters and spaces.")]
    public required string Name { get; set; }
}
