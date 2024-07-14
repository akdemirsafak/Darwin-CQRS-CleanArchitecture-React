namespace Darwin.Membership.API.Models.Plan;

public record CreatePlanRequest(string Name, string? Description, decimal Price, bool IsActive = true);