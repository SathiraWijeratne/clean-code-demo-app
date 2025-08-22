using System.ComponentModel.DataAnnotations;
using Application.Commands;

namespace Test.UnitTests.ValidationTests;

/// <summary>
/// Validation tests for the UpdateUserCommand class.
/// </summary>
public class UpdateUserCommandValidationTests
{
    // Test for valid name and ID
    [Fact]
    public void UpdateUserCommand_ValidNameAndId_ShouldPassValidation()
    {
        var command = new UpdateUserCommand { Id = 1, Name = "Jane Smith" };
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(command);

        var isValid = Validator.TryValidateObject(command, validationContext, validationResults, true);
        
        Assert.True(isValid);
        Assert.Empty(validationResults);
    }

    // Test for empty name
    [Fact]
    public void UpdateUserCommand_EmptyName_ShouldFailValidation()
    {
        var command = new UpdateUserCommand { Id = 1, Name = "" };
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(command);
        
        var isValid = Validator.TryValidateObject(command, validationContext, validationResults, true);
        
        Assert.False(isValid);
        Assert.Single(validationResults);
        Assert.Contains("The Name field is required", validationResults[0].ErrorMessage);
    }

    // Test for name with special characters
    [Fact]
    public void UpdateUserCommand_NameWithSpecialCharacters_ShouldFailValidation()
    {
        var command = new UpdateUserCommand { Id = 1, Name = "John@Doe" };
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(command);
        
        var isValid = Validator.TryValidateObject(command, validationContext, validationResults, true);
        
        Assert.False(isValid);
        Assert.Single(validationResults);
        Assert.Contains("Name must contain only letters and spaces", validationResults[0].ErrorMessage);
    }
}
