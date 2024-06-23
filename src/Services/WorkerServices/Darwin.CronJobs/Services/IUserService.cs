using Darwin.CronJobs.Models;

namespace Darwin.CronJobs.Services;


public interface IUserService
{
    Task<List<GetUserDto>> GetUsersAsync();
}
