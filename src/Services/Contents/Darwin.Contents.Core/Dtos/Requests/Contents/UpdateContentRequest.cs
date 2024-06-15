namespace Darwin.Contents.Core.RequestModels.Contents;
public record UpdateContentRequest(Guid id, 
    string Name, 
    string Lyrics, 
    string ImageUrl);