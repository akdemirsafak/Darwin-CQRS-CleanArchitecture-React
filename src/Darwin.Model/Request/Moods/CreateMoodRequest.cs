using Microsoft.AspNetCore.Http;

namespace Darwin.Model.Request.Moods;

public record CreateMoodRequest(
    string Name, 
    IFormFile ImageFile, 
    bool IsUsable); 
