using Idvisory.IdentityServer.Infrastructure.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace Idvisory.IdentityServer.Infrastructure.Models;
public class ApplicationUser : IdentityUser
{
    private string _firstName;
    private string _lastName;
    private string? _address;
    private GenderEnum? _gender;
    private DateTimeOffset? _birthDate;
    public ApplicationUser()
    {
        _firstName = UserName;
        _lastName = UserName;
    }
    public ApplicationUser(string userName, string firstName, string lastName, GenderEnum gender,
                            DateTimeOffset birthdate) : base(userName)
    {
        _firstName = firstName;
        _lastName = lastName;
        _gender = gender;
        _birthDate = birthdate;
    }
    public string FirstName { get => _firstName; set => _firstName = value; }
    public string LastName { get => _lastName; set => _lastName = value; }
    public GenderEnum? Gender { get => _gender; set => _gender = value; }
    public DateTimeOffset? BirthDate { get => _birthDate; set => _birthDate = value; }
    public string? Address { get => _address; set => _address = value; }

    public string Name => _firstName + " " + _lastName;
    public DateOnly? Age => GetAge();


    public DateOnly? GetAge()
    {
        if (_birthDate == null) return null;
        TimeSpan difference = _birthDate.Value.Subtract(DateTimeOffset.UtcNow);
        DateTime age = DateTime.MinValue + difference;
        int ageInYears = age.Year - 1;
        int ageInMonths = age.Month - 1;
        int ageInDays = age.Day - 1;

        return new DateOnly(ageInYears, ageInMonths, ageInDays);
    }
}
