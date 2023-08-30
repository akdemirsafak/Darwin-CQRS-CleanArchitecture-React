namespace Darwin.Model.Request.Moods;

public record CreateMoodRequest(string Name, string ImageUrl, bool IsUsable);
