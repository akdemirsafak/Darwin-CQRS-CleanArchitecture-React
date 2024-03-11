using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.RequestModels;
using Darwin.Domain.ResponseModels.Moods;
using FluentValidation;

namespace Darwin.Application.Features.Moods.Queries;

public static class GetMoodList
{
    public record Query(GetPaginationListRequest Model) : IQuery<DarwinResponse<GetMoodListResponse>>;//, ICacheableQuery
    //{
    //    //public string CachingKey => "MoodListCached";
    //    //public double CacheTime => 0.5;
    //}

    public class QueryHandler(IMoodService _moodService) : IQueryHandler<Query, DarwinResponse<GetMoodListResponse>>
    {

        public async Task<DarwinResponse<GetMoodListResponse>> Handle(Query request, CancellationToken cancellationToken)
        {

            return DarwinResponse<GetMoodListResponse>.Success(await _moodService.GetListAsync(request.Model));
        }
        public class GetMoodListQueryValidator : AbstractValidator<Query>
        {
            public GetMoodListQueryValidator()
            {
                RuleFor(x => x.Model.Page).GreaterThanOrEqualTo(1);
                RuleFor(x => x.Model.PageSize).GreaterThanOrEqualTo(2);
            }
        }
    }
}
