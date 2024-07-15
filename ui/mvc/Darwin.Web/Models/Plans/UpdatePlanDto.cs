namespace Darwin.Web.Models.Plans;

public class UpdatePlanDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool? IsActive { get; set; } = true;
}
