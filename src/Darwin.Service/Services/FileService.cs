using Microsoft.AspNetCore.Http;

namespace Darwin.Service.Services;

public class FileService : IFileService
{
    public async Task<string> UploadImage(IFormFile image) //returning imageUrl
    {
        if (image is not null && image.Length > 0)
        {
            var path=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/photos",image.FileName);
            using var stream=new FileStream(path,FileMode.Create);
            await image.CopyToAsync(stream, CancellationToken.None);
            return image.FileName;
        }
        return null;
    }
}
