namespace Darwin.Domain.RequestModels.PlayLists;

public record UpdatePlayListRequest(string Name, string Description, bool IsPublic, bool IsUsable);
