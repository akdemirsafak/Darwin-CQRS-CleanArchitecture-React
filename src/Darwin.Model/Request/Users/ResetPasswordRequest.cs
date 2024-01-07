using System.ComponentModel.DataAnnotations;

namespace Darwin.Model.Request.Users;

public class ResetPasswordRequest {

    [DataType(DataType.Password)]
    public required string Password { get; set; }
    [DataType(DataType.Password)]
    [Compare("Password")]
    public required string PasswordConfirm { get; set; }
}
