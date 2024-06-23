using Darwin.Shared.Events;
using MassTransit;


namespace Darwin.CronJobs.Services;

public class NotificationService : INotificationService
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public NotificationService(ISendEndpointProvider sendEndpointProvider)
    {
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task SendNew5ContentsAsync(List<string> to, string title, string body)
    {
        var @event= new SendNewContentsEvent
        {
            To = to,
            Subject = title,
            Body = body

        };

        var sendEndpoint=await _sendEndpointProvider.GetSendEndpoint(new System.Uri("queue:send-newcontents-queue"));


        await sendEndpoint.Send<SendNewContentsEvent>(@event);
    }
}
