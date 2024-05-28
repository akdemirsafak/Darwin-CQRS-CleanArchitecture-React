namespace Darwin.FileService.Services;

public interface IFileService
{
    Task<string> UploadImage(IFormFile image);
}
