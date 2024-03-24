using Darwin.Domain.Azure;
using Microsoft.AspNetCore.Http;

namespace Darwin.Application.Services;

public interface IAzureBlobStorageService
{
    Task<List<BlobDto>> ListAsync(string containerName);
    Task<BlobResponseDto> UploadAsync(IFormFile blob, string containerName);
    Task<BlobDto> DownloadAsync(string fileName, string containerName);

    Task<BlobResponseDto> DeleteAsync(string fileName, string containerName);
}
