namespace Darwin.Domain.Dtos;

public record SendEmailModel(List<string> Bcc, string Title, string Body, bool isBodyHtml);
