namespace Darwin.Model.Request.PlayLists;

public record CreatePlayListRequest(string Name, string Description, bool IsPublic, bool IsUsable);

