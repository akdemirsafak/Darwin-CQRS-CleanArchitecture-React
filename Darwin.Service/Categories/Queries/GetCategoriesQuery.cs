using AutoMapper;
using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Categories;
using Darwin.Service.Common;

namespace Darwin.Service.Categories.Queries;

public class GetCategoriesQuery : IQuery<DarwinResponse<List<GetCategoryResponse>>>
{

    public class Handler : IQueryHandler<GetCategoriesQuery, DarwinResponse<List<GetCategoryResponse>>>
    {
        private readonly IGenericRepositoryAsync<Category> _repository;
        private readonly IMapper _mapper;

        public Handler(IGenericRepositoryAsync<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DarwinResponse<List<GetCategoryResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllAsync();

            return DarwinResponse<List<GetCategoryResponse>>.Success(_mapper.Map<List<GetCategoryResponse>>(categories),200);

        }
    }
}
