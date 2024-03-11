using Darwin.Application.Services;
using Darwin.Domain.Dtos;

namespace Darwin.Application.CronJobs;


public sealed class WeeklyContents
{
    private readonly IEmailService _emailService;
    private readonly IUserService _userService;
    private readonly IContentService _contentService;
    public WeeklyContents(IEmailService emailService, IUserService userService, IContentService contentService)
    {
        _emailService = emailService;
        _userService = userService;
        _contentService = contentService;
    }


    public async Task SendNew5Contents()
    {
        var users = await _userService.GetAllAsync();


        var newContents = await _contentService.GetNewContentsAsync(5);

        var newContent = string.Join("", newContents.Select(content => $"<li>{content.Name}</li>"));
        if (users is not null)
        {
            var to = users.Select(x => x.Email).ToList();
            var title = "Darwin'de Haftanın ennnn yenileri Birden fazla kişiye";
            var body = $@"
            <html>
                <body>

                    <h3>Haftanın en yeni içerikleri</h3>
                </br> </br>

                    <img src=""https://images.pexels.com/photos/2479312/pexels-photo-2479312.jpeg"" width=""500px"" alt=""Logo"" />

                </br> </br>

                    <ul>
                        {newContent}
                        <li>The Weeknd - After Hours</li>
                        <li>The Neighbourhood - SoftCore</li>
                        <li>Çimen Show Podcast</li>
                        <li>Meksika Açmazı Podcast</li>
                        <li>Babala Tv Podcast</li>
                    </ul>

                    </br></br>
                    <hr>
                    </br></br>

                    <footer>
                        <h4 >Haftaya görüşmek üzere...</h4>
                    </footer>
                </body>
            </html>
";

            await _emailService.SendNew5ContentsAsync(new SendEmailModel(to, title, body, true));
        }
    }
}
