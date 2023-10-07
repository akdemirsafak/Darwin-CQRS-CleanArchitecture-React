using Darwin.Core.BaseDto;
using Darwin.Infrastructure.DbContexts;
using Darwin.Model.Response.Musics;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Service.Features.Musics.Queries;

public static class GetMusicById
{

    public record Query(Guid Id) : IQuery<DarwinResponse<GetMusicByIdResponse>>;

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<GetMusicByIdResponse>>
    {
        private readonly DarwinDbContext _dbContext;


        public QueryHandler(DarwinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DarwinResponse<GetMusicByIdResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var music = await _dbContext.Musics.Include(x => x.Moods).Include(x => x.Categories).SingleOrDefaultAsync(x => x.Id == request.Id);

            return DarwinResponse<GetMusicByIdResponse>.Success(music.Adapt<GetMusicByIdResponse>());
        }
    }

    public class GetMusicByIdQueryValidator : AbstractValidator<Query>
    {
        public GetMusicByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }
}

