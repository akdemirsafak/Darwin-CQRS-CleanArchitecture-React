using Darwin.CronJobs.Models;


namespace Darwin.CronJobs.Services;

public interface IContentService
{
    Task<List<GetContentDto>> GetContentsAsync();
}
