using Microsoft.AspNetCore.Http;

namespace Darwin.Domain.RequestModels.Moods;

public record CreateMoodRequest(
    string Name,
    IFormFile ImageFile,
    bool? IsUsable = true);
