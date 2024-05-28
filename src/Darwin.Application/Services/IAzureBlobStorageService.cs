using Darwin.Share.Dtos;
using Darwin.Shared.Dtos.Azure;
using Microsoft.AspNetCore.Http;

namespace Darwin.Application.Services;

public interface IAzureBlobStorageService
{
    Task<DarwinResponse<List<BlobDto>>> ListAsync(string containerName); // inceleyelim
    Task<DarwinResponse<BlobResponseDto>> UploadAsync(IFormFile file, string containerName);
    Task<DarwinResponse<BlobResponseDto>> DeleteAsync(string fileName, string containerName);
}
