namespace Darwin.Domain.RequestModels.PlayLists;

public record CreatePlayListRequest(string Name, string Description, bool IsPublic, bool? IsUsable = true);

