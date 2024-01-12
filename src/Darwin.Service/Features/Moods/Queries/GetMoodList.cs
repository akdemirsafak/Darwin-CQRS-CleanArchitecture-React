using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Request;
using Darwin.Model.Response.Moods;
using Darwin.Service.Common;
using Darwin.Service.Helper;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Moods.Queries;

public static class GetMoodList
{
    public record Query(GetPaginationListRequest Model) : IQuery<DarwinResponse<GetMoodListResponse>>;

    public class QueryHandler(IGenericRepository<Mood> _moodRepository) : IQueryHandler<Query, DarwinResponse<GetMoodListResponse>>
    {

        public async Task<DarwinResponse<GetMoodListResponse>> Handle(Query request, CancellationToken cancellationToken)
        {

            var queryable=_moodRepository.GetList();
            Paginate<Mood> paginate= Paginate<Mood>.ToPagedList(queryable,request.Model.Page,request.Model.PageSize);

            return DarwinResponse<GetMoodListResponse>.Success(paginate.Adapt<GetMoodListResponse>());
        }
        public class GetMoodListQueryValidator : AbstractValidator<Query>
        {
            public GetMoodListQueryValidator()
            {
                RuleFor(x=>x.Model.Page).GreaterThanOrEqualTo(1);
                RuleFor(x=>x.Model.PageSize).GreaterThanOrEqualTo(2);
            }
        }
    }
}
