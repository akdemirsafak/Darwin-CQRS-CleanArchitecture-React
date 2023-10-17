namespace Darwin.Model.Response.ContentAgeRates;

public class GetContentAgeRateResponse
{
    public Guid Id { get; set; }
    public int Rate { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}
