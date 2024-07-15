using System.ComponentModel;

namespace Darwin.Web.Models.Plans;

public class CreatePlanDto
{
    [DisplayName("Adı")]
    public string Name { get; set; }
    [DisplayName("Açıklama")]
    public string? Description { get; set; }
    [DisplayName("Fiyat")]
    public decimal Price { get; set; }
    [DisplayName("Aktif mi?")]
    public bool? IsActive { get; set; } = true;
}
