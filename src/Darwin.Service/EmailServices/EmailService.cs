using Darwin.Core.Entities;
using Darwin.Service.Configures;
using Darwin.Service.Events.UserCreated;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Darwin.Service.EmailServices;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    public EmailService(IOptions<EmailSettings> options)
    {
        _emailSettings = options.Value;
    }
    public async Task SendNew5ContentsAsync(SendEmailModel model)
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

        mailMessage.To.Add("akdemirsafak@gmail.com");
        model.Bcc.ForEach(to => mailMessage.Bcc.Add(to));

        mailMessage.Subject = model.Title;
        mailMessage.Body = model.Body;


        mailMessage.IsBodyHtml = model.isBodyHtml;

        await smtpClient.SendMailAsync(mailMessage);
    }

    public async Task SendWellcomeWithConfirmationAsync(UserCreatedMailModel model)
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

        mailMessage.To.Add(model.To);

        mailMessage.Subject = "Darwin'in dünyasına hoşgeldin.";
        mailMessage.Body = $@"
        <body>
</br></br>
            <h2>Hifi kalitesinde mükemmel içerikler burada.</h2>
            <h3>Yüksek kalitede müzik ve podcast'ler dinleyebilir ve yepyeni keşifler yapabilirsin. Bunun yanı sıra listeler oluşturabilir, yaş kısıtı modunu kullanarak kendi yaşına veya varisi olduğun kişilere özel içeriklerin gösterilmesini sağlayabilirsin.</h3>
</br>
             <h4> <a href=""{model.confirmationAddress}""> bu yazıya tıklayarak hesabını doğrulayabilirsin.</a></h4>
</br></br>

            <img src=""https://thumbs.dreamstime.com/b/welcome-poster-spectrum-brush-strokes-white-background-colorful-gradient-brush-design-vector-paper-illustration-r-welcome-125370796.jpg"" width=""500px"" height=""200px"">
</br>
            <p> Aramıza {model.createdAt} tarihinde katıldığını hatırlatmak isteriz.</p>
</br></br>
        
<hr>
</br></br>
            <footer> Keyifli günler..</footer>
        <body>
";


        mailMessage.IsBodyHtml = true;

        await smtpClient.SendMailAsync(mailMessage);
    }

    public async Task SendConfirmMailAsync(string To, string confirmationAddress)
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

        mailMessage.Subject = "Darwin Hesap Doğrulama.";
        mailMessage.Body = $@"
        <body>
</br></br>

             <h2> <a href=""{confirmationAddress}""> bu yazıya tıklayarak hesabınızı doğrulayabilirsiniz.</a></h2>
           
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
            <h2>Şifrenizi unuttuğunuzu belirttiniz..</h2>
          
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
