using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Categories;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Categories.Queries;

public class GetCategoriesQuery : IQuery<DarwinResponse<List<GetCategoryResponse>>>
{

    public class Handler : IQueryHandler<GetCategoriesQuery, DarwinResponse<List<GetCategoryResponse>>>
    {
        private readonly IGenericRepositoryAsync<Category> _repository;

        public Handler(IGenericRepositoryAsync<Category> repository)
        {
            _repository = repository;
        }

        public async Task<DarwinResponse<List<GetCategoryResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllAsync();

            return DarwinResponse<List<GetCategoryResponse>>.Success(categories.Adapt<List<GetCategoryResponse>>(), 200);

        }
    }
}
