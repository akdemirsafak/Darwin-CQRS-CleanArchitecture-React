using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Core.Dtos.Responses.Category;
using Darwin.Contents.Service.Common;
using Darwin.Shared.Dtos;
using FluentValidation;


namespace Darwin.Contents.Service.Features.Categories.Queries;

public static class GetCategoryById
{
    public record Query(Guid Id) : IQuery<DarwinResponse<GetCategoryResponse>>;

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<GetCategoryResponse>>
    {
        private readonly ICategoryService _categoryService;

        public QueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<DarwinResponse<GetCategoryResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetByIdAsync(request.Id);
            return DarwinResponse<GetCategoryResponse>.Success(category);
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


