namespace Darwin.Model.Request.PlayLists;

public record UpdatePlayListRequest(string Name, string Description, bool IsPublic, bool IsUsable);
