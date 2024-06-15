using AutoMapper;
using Darwin.Contents.Core.Dtos.Responses.Content;
using Darwin.Contents.Core.Entities;
using Darwin.Contents.Core.RequestModels.Contents;
using Darwin.Contents.Service.Helper;
using Darwin.Contents.Shared.Events.Contents;

namespace Darwin.Contents.Service.Mappers;

public class ContentMapper : Profile
{
    public ContentMapper()
    {
        CreateMap<CreateContentRequest, ContentCreatedEventSourcingEvent>();
        CreateMap<Content, CreatedContentResponse>();
        CreateMap<Content, UpdatedContentResponse>();
        CreateMap<Content, SearchContentResponse>().ReverseMap();
        CreateMap<Content, GetContentResponse>();
        CreateMap<Paginate<Content>, GetContentListResponse>();

    }
}