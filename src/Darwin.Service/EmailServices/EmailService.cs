using Darwin.Service.Configures;
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
}
