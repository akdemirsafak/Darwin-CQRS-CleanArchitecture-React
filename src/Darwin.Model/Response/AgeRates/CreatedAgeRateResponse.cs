namespace Darwin.Model.Response.AgeRates;

public class CreatedAgeRateResponse
{
    public Guid Id { get; set; }
    public int Rate { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}
