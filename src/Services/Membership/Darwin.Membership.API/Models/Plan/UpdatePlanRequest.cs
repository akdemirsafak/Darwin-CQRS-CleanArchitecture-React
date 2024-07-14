namespace Darwin.Membership.API.Models.Plan;

public record UpdatePlanRequest(string Name, string? Description, decimal Price, bool IsActive = true);