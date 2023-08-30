using AutoMapper;
using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Common;
using Darwin.Model.Request.Categories;
using Darwin.Model.Response.Categories;
using Darwin.Service.Common;

namespace Darwin.Service.Categories.Commands.Create;

public class CreateCategoryCommand : ICommand<DarwinResponse<CreatedCategoryResponse>>
{
    public CreateCategoryRequest Model { get; }

    public CreateCategoryCommand(CreateCategoryRequest model)
    {
        Model = model;
    }

    public class Handler : ICommandHandler<CreateCategoryCommand, DarwinResponse<CreatedCategoryResponse>>
    {
        private readonly IGenericRepositoryAsync<Category> _repository;
        private readonly IMapper _mapper;

        public Handler(IGenericRepositoryAsync<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DarwinResponse<CreatedCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity= _mapper.Map<Category>(request.Model);
            await _repository.CreateAsync(entity);
            return DarwinResponse<CreatedCategoryResponse>.Success(_mapper.Map<CreatedCategoryResponse>(entity),201);
        }
    }
}