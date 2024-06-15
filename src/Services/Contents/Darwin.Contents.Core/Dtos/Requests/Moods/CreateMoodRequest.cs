using Microsoft.AspNetCore.Http;

namespace Darwin.Contents.Core.RequestModels.Moods;

public record CreateMoodRequest(
    string Name,
    IFormFile ImageFile);
