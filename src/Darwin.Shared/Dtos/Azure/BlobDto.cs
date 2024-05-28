namespace Darwin.Shared.Dtos.Azure;

public class BlobDto
{
    public string? Name { get; set; }
    public string? Url { get; set; }
    public string? ContentType { get; set; }
    public Stream? Content { get; set; }
}
