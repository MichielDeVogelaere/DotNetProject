using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Routing;
using Test_Project.Controllers;

namespace Test_Project.Models {
public class PersonModel
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
    public string Name { get; set; }


    [Required(ErrorMessage = "First Name is required.")]
    public string FirstName { get; set; }


    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }


    [Required(ErrorMessage = "Country is required.")]
    [BelgiumResident(ErrorMessage = "You must be a Belgian resident.")]
    public string Country { get; set; }


    [Required(ErrorMessage = "Zip Code is required.")]
    [BelgiumZipCode(ErrorMessage = "Invalid Belgian Zip Code.")]
    public string ZipCode { get; set; }


    [Required(ErrorMessage = "Age is required.")]
    [AdultValidator(ErrorMessage = "You must be at least 18 years old.")]
    public int Age { get; set; }


    [Required(ErrorMessage = "Mobile Phone is required.")]
    [MobilePhoneValidator(ErrorMessage = "Invalid mobile phone format.")]
    public string MobilePhone { get; set; }
}

public class BelgiumResidentAttribute : ValidationAttribute // No Database configured so client-side validation 
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        string country = value as string;

        if (string.Equals(country, "Belgium", StringComparison.OrdinalIgnoreCase))
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult("You must be a resident of Belgium.");
        }
    }
}

public class BelgiumZipCodeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
            if (value is string zipCode && zipCode.Length == 4 && zipCode.All(char.IsDigit))
            {
                int zipCodeValue = int.Parse(zipCode);
                if (zipCodeValue >= 1000 && zipCodeValue <= 9999)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage ?? "Invalid Belgian Zip Code.");
    }
}

public class AdultValidatorAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if ((int)value >= 18)
        {
            return ValidationResult.Success;
        }

    return new ValidationResult(ErrorMessage ?? "You must be at least 18 years old.");
    }   
}

public class MobilePhoneValidatorAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var mobilePhone = value as string;

        if (mobilePhone != null && (mobilePhone.StartsWith("032") || mobilePhone.StartsWith("+32")) && mobilePhone.Length == 12)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage ?? "Invalid mobile phone format.");
    }
}
}