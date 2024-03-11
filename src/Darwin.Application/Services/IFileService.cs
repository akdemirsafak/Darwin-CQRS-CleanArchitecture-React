using Microsoft.AspNetCore.Http;

namespace Darwin.Application.Services;

public interface IFileService
{
    Task<string> UploadImage(IFormFile image);
}
