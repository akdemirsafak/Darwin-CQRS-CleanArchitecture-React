namespace Darwin.FileService.Services;

public class FileService : IFileService
{
    public async Task<string> UploadImage(IFormFile image) //returning imageUrl
    {
        if (image is not null && image.Length > 0)
        {
            var extent = Path.GetExtension(image.FileName);
            var randomName = ($"{Guid.NewGuid()}{extent}");
            var path=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/photos",randomName);
            using var stream=new FileStream(path,FileMode.Create);
            await image.CopyToAsync(stream, CancellationToken.None);
            return randomName;
        }
        return null;
    }
}
