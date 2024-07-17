using System.ComponentModel.DataAnnotations;

namespace Darwin.Web.Models;

public class LoginDto
{
    [Required]
    [EmailAddress]
    [Display(Name = "E-posta")]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Parola")]
    public string Password { get; set; }
}
