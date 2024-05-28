using Darwin.Notification.Settings;
using Darwin.Shared.Events;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Darwin.Notification.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    public EmailService(IOptions<EmailSettings> settings)
    {
        _emailSettings = settings.Value;
    }

    public async Task SendWellcomeEmailAsync(UserCreatedEvent mailDetails)
    {
        var smtpClient=new SmtpClient();
        smtpClient.Host = _emailSettings.Host;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.EnableSsl = true;
        smtpClient.Port = 587;
        smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);

        var mailMessage= new MailMessage();
        mailMessage.From = new MailAddress(_emailSettings.Email);

        mailMessage.To.Add(mailDetails.Email);

        mailMessage.Subject = "Darwin'in dünyasına hoşgeldin.";
        mailMessage.Body = $@"
                        <body>
                            </br></br>
                            <h2>Hifi kalitesinde mükemmel içerikler burada.</h2>
                            <h3>Yüksek kalitede müzik ve podcast'ler dinleyebilir ve yepyeni keşifler yapabilirsin. Bunun yanı sıra listeler oluşturabilir, yaş kısıtı modunu kullanarak kendi yaşına veya varisi olduğun kişilere özel içeriklerin gösterilmesini sağlayabilirsin.</h3>
                             </br>
                             <h4> <a href=""{mailDetails.EmailConfirmationLink}""> bu yazıya tıklayarak hesabını doğrulayabilirsin.</a></h4>
                            </br></br>

                            <img src=""https://thumbs.dreamstime.com/b/welcome-poster-spectrum-brush-strokes-white-background-colorful-gradient-brush-design-vector-paper-illustration-r-welcome-125370796.jpg"" width=""500px"" height=""200px"">
                            </br>
                            <p> Aramıza {mailDetails.CreatedDate} tarihinde katıldığını hatırlatmak isteriz.</p>
                            </br></br>
        
                            <hr>
                            </br></br>
                            Yukarıdaki linke tıklayamıyorsan bu yazıyı tarayıcına yapıştırarak hesabını onaylayabilirsin : {mailDetails.EmailConfirmationLink}
                            </br></br>

                            <footer> Keyifli günler..</footer>
                        <body>
                ";


        mailMessage.IsBodyHtml = true;

        await smtpClient.SendMailAsync(mailMessage);
    }






    public async Task SendNewContentsAsync(SendNewContentsEvent sendNewContentsEvent)
    {
        var smtpClient=new SmtpClient();
        smtpClient.Host = _emailSettings.Host;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.EnableSsl = true;
        smtpClient.Port = 587;
        smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);

        var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(_emailSettings.Email);
        mailMessage.To.Add(_emailSettings.Email);//Mesajı kendime yollayıp cc'ye kullanıcıları ekleyeceğim.
        sendNewContentsEvent.To.ForEach(to => mailMessage.Bcc.Add(to)); // Bcc'ye eklenen alıcıları diğer alıcılar göremeyecek.
        mailMessage.Subject = sendNewContentsEvent.Subject;
        mailMessage.Body = sendNewContentsEvent.Body;
        mailMessage.IsBodyHtml = true;

        await smtpClient.SendMailAsync(mailMessage);
    }

    public async Task SendVerifyEmailAsync(VerifyEmailEvent verifyEmailMessage)
    {
        var smtpClient=new SmtpClient();
        smtpClient.Host = _emailSettings.Host;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.EnableSsl = true;
        smtpClient.Port = 587;
        smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);

        var mailMessage= new MailMessage();
        mailMessage.From = new MailAddress(_emailSettings.Email);

        mailMessage.To.Add(verifyEmailMessage.To);

        mailMessage.Subject = "Darwin Hesap Doğrulama.";
        mailMessage.Body = $@"
        <body>
    </br></br>
            <h2>Merhaba {verifyEmailMessage.FullName}, {verifyEmailMessage.UserName} kullanıcı adlı hesabını aşağıdaki linke tıklayarak doğrulayabilirsiniz.</h2>
</br></br>

             <h2> <a href=""{verifyEmailMessage.ConfirmationUrl}""> bu yazıya tıklayarak hesabınızı doğrulayabilirsiniz.</a></h2>
           
</br></br>
            <footer> Keyifli günler..</footer>
        <body>
";


        mailMessage.IsBodyHtml = true;

        await smtpClient.SendMailAsync(mailMessage);
    }


    public async Task SendResetPasswordMailAsync(string To, string resetPasswordTokenAddress)
    {
        var smtpClient=new SmtpClient();
        smtpClient.Host = _emailSettings.Host;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.EnableSsl = true;
        smtpClient.Port = 587;
        smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);

        var mailMessage= new MailMessage();
        mailMessage.From = new MailAddress(_emailSettings.Email);

        mailMessage.To.Add(To);

        mailMessage.Subject = "Darwin Hesap Şifreni sıfırla";
        mailMessage.Body = $@"
            <body>
    </br></br>
                <h2>Sayın : {To}, Şifrenizi unuttuğunuzu belirttiniz..</h2>

    </br>
                 <h4> <a href=""{resetPasswordTokenAddress}""> bu yazıya tıklayarak şifrenizi yeniden tanımlayabilirsiniz.</a></h4>

    </br></br>
                <footer> Keyifli günler..</footer>
            <body>
    ";

        mailMessage.IsBodyHtml = true;

        await smtpClient.SendMailAsync(mailMessage);
    }
}
