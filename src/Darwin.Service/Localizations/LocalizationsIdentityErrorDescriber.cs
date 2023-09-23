using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Localizations;

public class LocalizationsIdentityErrorDescriber: IdentityErrorDescriber
{
    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError {Code = "DuplicateEmail", Description=$"'{email} zaten kullanılıyor.'"};
    }
    public override IdentityError InvalidEmail(string? email)
    {
        return new IdentityError { Code = "InvalidEmail", Description = $"'{email}' kullanılamaz." };
    }
    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError { Code = "DuplicateUserName", Description = $"'{userName} zaten kullanılıyor.'" };
    }
    public override IdentityError InvalidUserName(string? userName)
    {
        return new IdentityError { Code = "InvalidUserName", Description = $"'{userName}' kullanılamaz." };
    }
    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError { Code = "PasswordTooShort", Description = "Parola en az 6 karakter olmalıdır." };
    }
}
