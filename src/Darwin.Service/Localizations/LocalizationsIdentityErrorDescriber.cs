using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Localizations;

public class LocalizationsIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError { Code = "DuplicateEmail", Description = $"'{email} email adresi zaten kullanılıyor.'" };
    }
    public override IdentityError InvalidEmail(string? email)
    {
        return new IdentityError { Code = "InvalidEmail", Description = $"'{email}' email adresi kullanılamaz." };
    }
    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError { Code = "DuplicateUserName", Description = $"'{userName} kullanıcı adı zaten kullanılıyor.'" };
    }
    public override IdentityError InvalidUserName(string? userName)
    {
        return new IdentityError { Code = "InvalidUserName", Description = $"'{userName}' kullanıcı adı kullanılamaz." };
    }
    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError { Code = "PasswordTooShort", Description = "Parola en az 6 karakter olmalıdır." };
    }
}
