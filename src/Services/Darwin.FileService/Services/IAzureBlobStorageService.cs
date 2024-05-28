using Darwin.Share.Dtos;
using Darwin.Shared.Dtos.Azure;

namespace Darwin.FileService.Services;

public interface IAzureBlobStorageService
{
    Task<DarwinResponse<List<BlobDto>>> ListAsync(string containerName);
    Task<DarwinResponse<BlobResponseDto>> UploadAsync(IFormFile file, string containerName);
    Task<DarwinResponse<BlobResponseDto>> DeleteAsync(string fileName, string containerName);
}
