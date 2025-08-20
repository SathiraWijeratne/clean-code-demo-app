using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CreateUserDto
    {
    [Required]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters.")]
    [RegularExpression(@"^[a-zA-Z]+(?:\\s[a-zA-Z]+)*$", ErrorMessage = "Name must contain only letters and spaces.")]
    public required string Name { get; set; }
    }
}