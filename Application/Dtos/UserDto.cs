using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos;
/// <summary>
/// Data transfer object for user information.
/// </summary>
public class UserDto
{
    public int Id { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters.")]
    [RegularExpression(@"^[a-zA-Z]+(?:\\s[a-zA-Z]+)*$", ErrorMessage = "Name must contain only letters and spaces.")]
    public required string Name { get; set; }
}
