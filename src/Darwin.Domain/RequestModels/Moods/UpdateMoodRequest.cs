namespace Darwin.Domain.RequestModels.Moods;

public record UpdateMoodRequest(string Name, string ImageUrl, bool IsUsable);
