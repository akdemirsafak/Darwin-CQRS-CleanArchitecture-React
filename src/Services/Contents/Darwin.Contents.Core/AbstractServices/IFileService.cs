using Darwin.Shared.Dtos;
using Darwin.Shared.Dtos.Azure;
using Microsoft.AspNetCore.Http;

namespace Darwin.Contents.Core.AbstractServices;

public interface IFileService
{
    Task<DarwinResponse<List<BlobDto>>> ListAsync(string containerName); // inceleyelim
    Task<DarwinResponse<BlobResponseDto>> UploadAsync(IFormFile file, string containerName);
    Task<DarwinResponse<BlobResponseDto>> DeleteAsync(string fileName, string containerName);
}
