using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response;
using Darwin.Model.Response.Contents;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Service.Features.Contents.Queries;

public static class GetContentList
{
    public record Query(int Page,int PageSize) : IQuery<DarwinResponse<GetContentListResponse>>;
    public class QueryHandler : IQueryHandler<Query, DarwinResponse<GetContentListResponse>>
    {
        private readonly IContentRepository _contentRepository;

        public QueryHandler(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public async Task<DarwinResponse<GetContentListResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            //var queryAbleContents=_contentRepository.GetList();

            //var contents = await PagedList<Content>.ToPagedListAsync(queryAbleContents, request.Page, request.PageSize);

            //var pagedGetContentResponse=contents.Adapt<PagedList<GetContentResponse>>();


            var queryAbleContents=_contentRepository.GetList();
            int totalCount= queryAbleContents.Count();
            int currentPage=request.Page;
            int pageSize=request.PageSize;
            int totalPages=(int)Math.Ceiling(totalCount/(double)pageSize);
            var items=await queryAbleContents
                .Skip((currentPage-1)*pageSize)
                .Take(pageSize)
                .ToListAsync();
            var response= new GetContentListResponse()
            {
                CurrentPage=currentPage,
                PageSize=pageSize,
                TotalCount=totalCount,
                TotalPages=totalPages,
                Items=items.Adapt<List<GetContentResponse>>()
            };

            return DarwinResponse<GetContentListResponse>.Success(response);
        }
    }
    public class GetPaginableContentQueryValidator : AbstractValidator<Query>
    {
        public GetPaginableContentQueryValidator()
        {
            RuleFor(x=>x.Page).GreaterThanOrEqualTo(1);
            RuleFor(x=>x.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
