using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Darwin.Shared.Dtos;
using Darwin.Shared.Dtos.Azure;

namespace Darwin.FileService.Services;

public class AzureBlobStorageService : IAzureBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public AzureBlobStorageService(
        BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<DarwinResponse<BlobResponseDto>> DeleteAsync(string fileName, string containerName)
    {
        var blobContainter=_blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient file = blobContainter.GetBlobClient(fileName);
        bool deleteResult=await file.DeleteIfExistsAsync();
        if (!deleteResult)
        {
            return DarwinResponse<BlobResponseDto>.Fail("Dosya silinirken bir problem yaşandı.");
        }
        return DarwinResponse<BlobResponseDto>.Success(new BlobResponseDto { IsSuccess = true, Status = $"File {fileName} Deleted Successfully." }, 200);
    }


    public async Task<DarwinResponse<List<BlobDto>>> ListAsync(string containerName)
    {
        List<BlobDto> files= new();

        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        await foreach (var blobItem in blobContainerClient.GetBlobsAsync())
        {
            string uri=blobContainerClient.Uri.ToString();
            string name=blobItem.Name;
            var fullUri = $"{uri}/{name}";
            files.Add(new BlobDto
            {
                Name = name,
                Url = fullUri,
                ContentType = blobItem.Properties.ContentType
            });
        }
        return DarwinResponse<List<BlobDto>>.Success(files, 200);
    }

    #region UploadAsync
    public async Task<DarwinResponse<BlobResponseDto>> UploadAsync(IFormFile file, string containerName)
    {
        BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        blobContainerClient.CreateIfNotExists(PublicAccessType.BlobContainer);

        BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);
        await using var data = file.OpenReadStream();
        await blobClient.UploadAsync(data, true);

        return DarwinResponse<BlobResponseDto>.Success(GenerateBlobResponseDto(blobClient, file), 201);
    }
    #endregion

    #region UploadAsync'de Response için
    private BlobResponseDto GenerateBlobResponseDto(BlobClient blobClient, IFormFile file)
    {
        return new BlobResponseDto
        {
            IsSuccess = true,
            Status = $"File {file.FileName} Uploaded Successfully.",
            Blob = new BlobDto
            {
                Name = blobClient.Name,
                Url = blobClient.Uri.AbsoluteUri,
                ContentType = file.ContentType
            }
        };
    }
    #endregion

}
