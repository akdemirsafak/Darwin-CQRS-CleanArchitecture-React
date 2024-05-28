using Darwin.Application.Services;
using Darwin.Share.Dtos;
using Darwin.Shared.Dtos.Azure;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Darwin.Infrastructure.Services;

public class AzureBlobStorageService : IAzureBlobStorageService
{
    private readonly HttpClient _httpClient;

    public AzureBlobStorageService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    #region DeleteAsync

    public async Task<DarwinResponse<BlobResponseDto>> DeleteAsync(string fileName, string containerName)
    {

        var clientResponse= await _httpClient.DeleteAsync($"blobs/delete?fileName={fileName}&containerName={containerName}");
        var responseData=await clientResponse.Content.ReadFromJsonAsync<BlobResponseDto>();
        if (!clientResponse.IsSuccessStatusCode)
        {
            return DarwinResponse<BlobResponseDto>.Fail(responseData.Status, 500);
        }

        return DarwinResponse<BlobResponseDto>.Success(new BlobResponseDto { IsSuccess = true, Status = $"File {fileName} Deleted Successfully." });
    }
    #endregion
    public async Task<DarwinResponse<List<BlobDto>>> ListAsync(string containerName)
    {

        var response=await _httpClient.GetFromJsonAsync<DarwinResponse<List<BlobDto>>>($"blobs/listblobs?containerName={containerName}");

        return DarwinResponse<List<BlobDto>>.Success(response.Data);

    }

    #region UploadAsync
    public async Task<DarwinResponse<BlobResponseDto>> UploadAsync(IFormFile file, string containerName)
    {

        if (file == null || file.Length == 0)
        {
            return DarwinResponse<BlobResponseDto>.Fail("Dosya geçersiz.");
        }


        using (var content = new MultipartFormDataContent())
        {
            var randomFilename = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new ByteArrayContent(ms.ToArray()), "file", randomFilename);
            multipartContent.Add(new StringContent(containerName), "containerName");

            var clientResponse = await _httpClient.PostAsync("blobs/upload", multipartContent);
            var response= await clientResponse.Content.ReadFromJsonAsync<DarwinResponse<BlobResponseDto>>();
            if (!clientResponse.IsSuccessStatusCode)
            {
                return DarwinResponse<BlobResponseDto>.Fail(response.Data.Status);
            }
            return DarwinResponse<BlobResponseDto>.Success(response.Data, 201);

        }
    }
    #endregion
}
