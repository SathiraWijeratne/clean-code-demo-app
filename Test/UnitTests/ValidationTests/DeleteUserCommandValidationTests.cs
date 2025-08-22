using System.ComponentModel.DataAnnotations;
using Application.Commands;

namespace Test.UnitTests.ValidationTests;

/// <summary>
/// Validation tests for the DeleteUserCommand class.
/// </summary>
public class DeleteUserCommandValidationTests
{
    // Test for valid ID
    [Fact]
    public void DeleteUserCommand_ValidId_ShouldPassValidation()
    {
        var command = new DeleteUserCommand { Id = 1 };
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(command);
        
        var isValid = Validator.TryValidateObject(command, validationContext, validationResults, true);
        
        Assert.True(isValid);
        Assert.Empty(validationResults);
    }

    // Test for zero ID
    [Fact]
    public void DeleteUserCommand_ZeroId_ShouldFailValidation()
    {
        
        var command = new DeleteUserCommand { Id = 0 };
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(command);
        
        var isValid = Validator.TryValidateObject(command, validationContext, validationResults, true);
        
        Assert.False(isValid);
        Assert.Single(validationResults);
        Assert.Contains("Invalid user ID", validationResults[0].ErrorMessage);
    }

    // Test for negative ID
    [Fact]
    public void DeleteUserCommand_NegativeId_ShouldFailValidation()
    {
        
        var command = new DeleteUserCommand { Id = -1 };
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(command);
        
        var isValid = Validator.TryValidateObject(command, validationContext, validationResults, true);

        Assert.False(isValid);
        Assert.Single(validationResults);
        Assert.Contains("Invalid user ID", validationResults[0].ErrorMessage);
    }
}
