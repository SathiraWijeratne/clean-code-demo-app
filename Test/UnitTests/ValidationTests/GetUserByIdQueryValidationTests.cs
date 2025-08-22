using System.ComponentModel.DataAnnotations;
using Application.Queries;

namespace Test.UnitTests.ValidationTests;

/// <summary>
/// Validation tests for the GetUserByIdQuery class.
/// </summary>
public class GetUserByIdQueryValidationTests
{
    // Test for valid ID
    [Fact]
    public void GetUserByIdQuery_ValidId_ShouldPassValidation()
    {
        var query = new GetUserByIdQuery { Id = 1 };
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(query);
        
        var isValid = Validator.TryValidateObject(query, validationContext, validationResults, true);
        
        Assert.True(isValid);
        Assert.Empty(validationResults);
    }

    // Test for zero ID
    [Fact]
    public void GetUserByIdQuery_ZeroId_ShouldFailValidation()
    {
        var query = new GetUserByIdQuery { Id = 0 };
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(query);
        
        var isValid = Validator.TryValidateObject(query, validationContext, validationResults, true);
        
        Assert.False(isValid);
        Assert.Single(validationResults);
        Assert.Contains("Id must be a positive integer", validationResults[0].ErrorMessage);
    }

    // Test for negative ID
    [Fact]
    public void GetUserByIdQuery_NegativeId_ShouldFailValidation()
    {
        
        var query = new GetUserByIdQuery { Id = -5 };
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(query);
        
        var isValid = Validator.TryValidateObject(query, validationContext, validationResults, true);
        
        Assert.False(isValid);
        Assert.Single(validationResults);
        Assert.Contains("Id must be a positive integer", validationResults[0].ErrorMessage);
    }
}
