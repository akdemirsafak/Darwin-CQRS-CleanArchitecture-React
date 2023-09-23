namespace Darwin.Service.Helpers;

public static class UserNameGeneratorThanEmail
{
    public static string Generate(string email)
    {
        var atIndex = email.IndexOf("@");
        email = email.Substring(0, atIndex);
        var random = new Random();
        return email + random.Next(1234, 9876);
    }
}
