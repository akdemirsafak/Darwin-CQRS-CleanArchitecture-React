using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Categories;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Categories.Queries;

public class GetCategoryByIdQuery : IQuery<DarwinResponse<GetCategoryResponse>>
{
    public Guid Id { get; }

    public GetCategoryByIdQuery(Guid id)
    {
        Id = id;
    }

    public class Handler : IQueryHandler<GetCategoryByIdQuery, DarwinResponse<GetCategoryResponse>>
    {
        private readonly IGenericRepository<Category> _repository;


        public Handler(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<DarwinResponse<GetCategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetAsync(x=>x.Id==request.Id);
            return DarwinResponse<GetCategoryResponse>.Success(category.Adapt<GetCategoryResponse>());
        }
    }
}
