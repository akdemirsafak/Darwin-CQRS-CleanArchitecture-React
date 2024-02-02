using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Darwin.Model.Azure;
using Microsoft.AspNetCore.Http;

namespace Darwin.Service.Services;

public class AzureBlobStorageService : IAzureBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public AzureBlobStorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<BlobResponseDto> DeleteAsync(string fileName, string containerName)
    {
        var blobContainter=_blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient file = blobContainter.GetBlobClient(fileName);
        await file.DeleteIfExistsAsync();
        return new BlobResponseDto
        {
            IsSuccess = true,
            Status = $"File {fileName} Deleted Successfully."
        };
    }

    public async Task<BlobDto> DownloadAsync(string fileName, string containerName)
    {
        BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient file = blobContainerClient.GetBlobClient(fileName);


        if (await file.ExistsAsync())
        {
            BlobDownloadInfo download = await file.DownloadAsync();
            return new BlobDto
            {
                Name = file.Name,
                ContentType = download.ContentType,
                Url = file.Uri.AbsoluteUri,
                Content = download.Content
            };
        }
        return null;

    }

    public async Task<List<BlobDto>> ListAsync(string containerName)
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
        return files;
    }

    public async Task<BlobResponseDto> UploadAsync(IFormFile blob, string containerName)
    {

        BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        blobContainerClient.CreateIfNotExists(PublicAccessType.BlobContainer);

        BlobClient file = blobContainerClient.GetBlobClient(blob.FileName);
        await using var data = blob.OpenReadStream();
        await file.UploadAsync(data, true);

        return new BlobResponseDto
        {
            IsSuccess = true,
            Status = $"File {blob.FileName} Uploaded Successfully.",
            Blob = new BlobDto
            {
                Name = file.Name,
                Url = file.Uri.AbsoluteUri,
                ContentType = blob.ContentType
            }
        };


        //BlobContainerClient _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        //BlobClient blobClient = _blobContainerClient.GetBlobClient(blob.FileName);

        ////await using var data = blob.OpenReadStream();
        ////await blobClient.UploadAsync(data, true);

        //await using (Stream? data= blob.OpenReadStream())
        //{
        //    await blobClient.UploadAsync(data);
        //}


        //return new BlobResponseDto{
        //    IsSuccess=true,
        //    Status=$"File {blob.FileName} Uploaded Successfully.",
        //    Blob=new BlobDto
        //    {
        //        Name=blobClient.Name,
        //        Url=blobClient.Uri.AbsoluteUri,
        //        ContentType=blob.ContentType
        //    }
        //};

    }

}
