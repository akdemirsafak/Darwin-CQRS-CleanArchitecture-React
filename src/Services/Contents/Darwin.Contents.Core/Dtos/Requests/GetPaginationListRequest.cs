namespace Darwin.Contents.Core.RequestModels;

public record GetPaginationListRequest(int Page = 1, int PageSize = 12);
