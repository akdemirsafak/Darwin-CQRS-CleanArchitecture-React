using Darwin.Application.Services;
using Darwin.Domain.Entities;
using Darwin.Shared.Events;
using MassTransit;

namespace Darwin.Application.CronJobs;


public sealed class WeeklyContents
{
    //private readonly IUserService _userService;
    private readonly IContentService _contentService;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    public WeeklyContents(
        //IUserService userService,
        IContentService contentService,
        ISendEndpointProvider sendEndpointProvider)
    {
        //_userService = userService;
        _contentService = contentService;
        _sendEndpointProvider = sendEndpointProvider;
    }


    public async Task SendNewContents()
    {
        //var users = await _userService.GetAllAsync();
        var users = new List<AppUser>();

        var newContents = await _contentService.GetNewContentsAsync(5);

        var newContent = string.Join("", newContents.Select(content => $"<li>{content.Name}</li>"));


        if (users is not null)
        {
            #region MailBody
            var to = users.Select(x => x.Email).ToList();
            var subject = "Darwin'de Haftanın ennnn yenileri ";
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

            #endregion

            var sender= await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:send-newcontents-queue"));
            await sender.Send(new SendNewContentsEvent
            {
                To = to,
                Subject = subject,
                Body = body
            });
        }
    }
}
