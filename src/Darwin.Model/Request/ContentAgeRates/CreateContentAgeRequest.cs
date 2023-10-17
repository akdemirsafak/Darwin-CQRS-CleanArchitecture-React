namespace Darwin.Model.Request.ContentAgeRates;

public record CreateContentAgeRequest(int Rate, string Name, string Description, bool IsActive);
