using Microsoft.AspNetCore.Http;

namespace Darwin.Domain.RequestModels.Contents;

public record CreateContentRequest(string Name, string Lyrics, IFormFile ImageFile, IList<Guid> SelectedCategories, IList<Guid> SelectedMoods, bool IsUsable=true);
