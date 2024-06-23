using Darwin.CronJobs.Services;

namespace Darwin.CronJobs.BackgroundJobs;

public class WeeklyContents
{
    private readonly IContentService _contentService;
    private readonly INotificationService _notificationService;
    private readonly IUserService _userService;

    public WeeklyContents(
        IContentService contentService,
        INotificationService notificationService,
        IUserService userService)
    {
        _contentService = contentService;
        _notificationService = notificationService;
        _userService = userService;
    }

    public async Task SendNew5Contents()
    {
        var users= await _userService.GetUsersAsync();

        if (users is null)
            throw new Exception("Kullanıcılar alınamadı.");

        var contents= await _contentService.GetContentsAsync();
        var topFiveContent= contents.Take(5);
        var newContent = string.Join("", contents.Select(content => $"<li>{content.Name}</li>"));


        var to=users.Select(x=>x.Email).ToList();
        var title="Darwin'in Ennnn Yenileri";
        var body=$@"
                    <html>
                        <body>

                            <h3> Haftanın en yeni içerikleri </h3>
                        </br> </br>

                            <img src = ""https://images.pexels.com/photos/2479312/pexels-photo-2479312.jpeg"" width=""500px"" alt=""Logo"" />

                        </br> </br>

                            <ul>
                                {newContent}
                                <li> The Weeknd - After Hours </ li>
                                <li> The Neighbourhood - SoftCore </li>
                                <li> Çimen Show Podcast</li>
                                <li> Meksika Açmazı Podcast</li>
                                <li> Babala Tv Podcast</li>
                            </ul>

                            </br></ br>
                            <hr>
                            </br ></br>

                            <footer>
                                <h4> Haftaya görüşmek üzere...</h4>
                            </footer>
                        </body>
                    </html>
        ";

        await _notificationService.SendNew5ContentsAsync(to, title, body);
    }
}
