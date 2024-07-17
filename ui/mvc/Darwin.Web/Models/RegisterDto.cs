using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Darwin.Web.Models;

public class RegisterDto
{
    [DisplayName("Ad : ")]
    public string Name { get; set; }
    [DisplayName("Soyad : ")]
    public string LastName { get; set; }
    [DisplayName("Email : ")]
    public string Email { get; set; }
    [DisplayName("Şifre : ")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DisplayName("Şifre Tekrar : ")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Şifre ve Şifre Tekrar alanları eşleşmelidir.")]
    public string ConfirmPassword { get; set; }
}
