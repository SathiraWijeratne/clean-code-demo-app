using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.Commands;

/// <summary>
/// Command for updating an existing user.
/// </summary>
public class UpdateUserCommand : IRequest
{
    public int Id { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters.")]
    [RegularExpression(@"^[a-zA-Z]+(?:\s[a-zA-Z]+)*$", ErrorMessage = "Name must contain only letters and spaces.")]
    public required string Name { get; set; }
}
