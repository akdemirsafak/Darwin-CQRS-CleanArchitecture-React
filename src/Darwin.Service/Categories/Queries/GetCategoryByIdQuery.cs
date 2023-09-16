using AutoMapper;
using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Categories;
using Darwin.Service.Common;

namespace Darwin.Service.Categories.Queries;

public class GetCategoryByIdQuery:IQuery<DarwinResponse<GetCategoryResponse>>
{
    public Guid Id { get; }

    public GetCategoryByIdQuery(Guid id)
    {
        Id = id;
    }

    public class Handler : IQueryHandler<GetCategoryByIdQuery, DarwinResponse<GetCategoryResponse>>
    {
        private readonly IGenericRepositoryAsync<Category> _repository;
        private readonly IMapper _mapper;

        public Handler(IGenericRepositoryAsync<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DarwinResponse<GetCategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetAsync(x=>x.Id==request.Id);
            return DarwinResponse<GetCategoryResponse>.Success(_mapper.Map<GetCategoryResponse>(category));
        }
    }
}
