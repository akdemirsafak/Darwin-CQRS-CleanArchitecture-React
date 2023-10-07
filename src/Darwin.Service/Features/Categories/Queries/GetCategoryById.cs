using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Categories;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Categories.Queries;

public static class GetCategoryById
{
    public record Query(Guid Id) : IQuery<DarwinResponse<GetCategoryResponse>>;

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<GetCategoryResponse>>
    {
        private readonly IGenericRepository<Category> _repository;

        public QueryHandler(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<DarwinResponse<GetCategoryResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetAsync(x => x.Id == request.Id);
            return DarwinResponse<GetCategoryResponse>.Success(category.Adapt<GetCategoryResponse>());
        }
    }
    public class GetCategoryByIdQueryValidator : AbstractValidator<Query>
    {
        public GetCategoryByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }
}


