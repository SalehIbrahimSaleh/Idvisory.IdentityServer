using System.ComponentModel.DataAnnotations;

namespace Matgr.Identity.Pages.Account.Register;
public class InputModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Gender { get; set; }
    public DateTimeOffset BirthDate { get; set; }
    [Required]
    public string Password { get; set; }
    public string ReturnUrl { get; set; }
}
