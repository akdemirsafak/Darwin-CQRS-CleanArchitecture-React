using Darwin.Core.Entities;
using Darwin.Infrastructure.DbContexts;
using Darwin.Service.Configures;
using Darwin.Service.EmailServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Service.Jobs;

public class WeeklyContents
{
    private readonly DarwinDbContext _dbContext;
    private readonly IEmailService _emailService;
    private readonly UserManager<AppUser> _userMager;

    public WeeklyContents(DarwinDbContext dbContext, IEmailService emailService, UserManager<AppUser> userMager)
    {
        _dbContext = dbContext;
        _emailService = emailService;
        _userMager = userMager;
    }

    public async Task SendNew5Contents()
    {
        var users=  await _userMager.Users.ToListAsync();
        var userSuggestion= await _dbContext.Contents.OrderByDescending(x=>x.CreatedAt).Take(5).ToListAsync();

        if (users is not null)
        {
            var to=users.Select(x=>x.Email).ToList();
            var title="Darwin'de Haftanın ennnn yenileri Birden fazla kişiye";
            var body=$@"
            <html>
                <body>

                    <h3>Haftanın en yeni içerikleri</h3>
                </br> </br>

                    <img src=""https://images.pexels.com/photos/2479312/pexels-photo-2479312.jpeg"" width=""500px"" alt=""Logo"" />

                </br> </br>

                    <ul>
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
