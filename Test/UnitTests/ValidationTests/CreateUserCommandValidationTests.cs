using System.ComponentModel.DataAnnotations;
using Application.Commands;

namespace Test.UnitTests.ValidationTests;

/// <summary>
/// Validation tests for the CreateUserCommand class.
/// </summary>
public class CreateUserCommandValidationTests
{
    // Test for valid name
    [Fact]
    public void CreateUserCommand_ValidName_ShouldPassValidation()
    {
        var command = new CreateUserCommand { Name = "John Doe" };
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(command);
        
        var isValid = Validator.TryValidateObject(command, validationContext, validationResults, true);
        
        Assert.True(isValid);
        Assert.Empty(validationResults);
    }

    // Test for empty name
    [Fact]
    public void CreateUserCommand_EmptyName_ShouldFailValidation()
    {
        
        var command = new CreateUserCommand { Name = "" };
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(command);
        
        var isValid = Validator.TryValidateObject(command, validationContext, validationResults, true);
        
        Assert.False(isValid);
        Assert.Single(validationResults);
        Assert.Contains("The Name field is required", validationResults[0].ErrorMessage);
    }

    // Test for name with numbers
    [Fact]
    public void CreateUserCommand_NameWithNumbers_ShouldFailValidation()
    {
        var command = new CreateUserCommand { Name = "John123" };
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(command);
        
        var isValid = Validator.TryValidateObject(command, validationContext, validationResults, true);
        
        Assert.False(isValid);
        Assert.Single(validationResults);
        Assert.Contains("Name must contain only letters and spaces", validationResults[0].ErrorMessage);
    }

    // Test for a name that's too long
    [Fact]
    public void CreateUserCommand_NameTooLong_ShouldFailValidation()
    {
        var longName = new string('A', 101); // 101 characters
        var command = new CreateUserCommand { Name = longName };
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(command);
        
        var isValid = Validator.TryValidateObject(command, validationContext, validationResults, true);
        
        Assert.False(isValid);
        Assert.Single(validationResults);
        Assert.Contains("Name must be between 1 and 100 characters", validationResults[0].ErrorMessage);
    }
}
