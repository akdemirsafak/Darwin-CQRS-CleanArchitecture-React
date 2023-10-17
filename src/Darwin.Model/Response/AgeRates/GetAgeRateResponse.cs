namespace Darwin.Model.Response.ContentAgeRates;

public class GetAgeRateResponse
{
    public Guid Id { get; set; }
    public int Rate { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}
