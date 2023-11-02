namespace Darwin.Service.Configures;

public record SendEmailModel(List<string> Bcc,string Title,string Body,bool isBodyHtml);