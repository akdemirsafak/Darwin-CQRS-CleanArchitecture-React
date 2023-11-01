using Darwin.Service.Configures;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace Darwin.Service.EmailServices;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    public EmailService(IOptions<EmailSettings> options)
    {
        _emailSettings = options.Value;
    }
    public async Task SendWeeklySuggestionsAsync(SendEmailModel model)
    {
        var smtpClient=new SmtpClient();
        smtpClient.Host = _emailSettings.Host;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.EnableSsl = true;
        smtpClient.Port = 587;
        smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);

        var mailMessage= new MailMessage();

        mailMessage.From = new MailAddress(_emailSettings.Email); //Kim yolluyor.
        mailMessage.To.Add(model.To); //Kime gidecek
        //mailMessage.Subject = model.Title; //Mail Başlığı
        //mailMessage.Body = model.Body;
        mailMessage.Subject = "Haftanın özeti bu hafta en çok dinlenenler :";
        mailMessage.Body = @$"
            <h1>Haftanın en çok dinlenenlerinde : </h1>
            <ul>
                <li> The Weeknd - Is There Someone Else? </li>
                <li> Ghost - Mary on a cross </li>
                <li> Post Malone - Circles </li>
                <li> The Weeknd The Hills</li>
            </ul>

</br></br>
<h3> Haftaya görüşmek üzere..</h3>
";    
        mailMessage.IsBodyHtml = true; //Html kodları barındırdığını belirttik.

        await smtpClient.SendMailAsync(mailMessage);
    }
}
