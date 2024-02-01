namespace Darwin.Model.Azure;

public class BlobResponseDto
{
    public BlobResponseDto()
    {

    }
    public BlobResponseDto(BlobDto blob)
    {
        Blob = blob;
    }
    public BlobDto Blob { get; set; }
    public string? Status { get; set; }
    public bool IsSuccess { get; set; }
}
