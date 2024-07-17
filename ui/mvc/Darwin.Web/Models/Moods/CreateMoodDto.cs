using System.ComponentModel;

namespace Darwin.Web.Models.Moods;

public class CreateMoodDto
{
    [DisplayName("Adı : ")]
    public string Name { get; set; }
    [DisplayName("Görsel : ")]
    public IFormFile ImageFile { get; set; }
}
