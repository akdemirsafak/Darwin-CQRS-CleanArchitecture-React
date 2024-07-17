using System.ComponentModel;

namespace Darwin.Web.Models.Categories;

public class CreateCategoryDto
{
    [DisplayName("Adı : ")]
    public string Name { get; set; }
    [DisplayName("Görsel : ")]
    public IFormFile ImageFile { get; set; }
}
