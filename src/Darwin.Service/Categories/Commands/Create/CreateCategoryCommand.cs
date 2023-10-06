using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Request.Categories;
using Darwin.Model.Response.Categories;
using Darwin.Service.Common;
using Mapster;

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
        private readonly IGenericRepository<Category> _repository;

        public Handler(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<DarwinResponse<CreatedCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity= request.Model.Adapt<Category>();
            await _repository.CreateAsync(entity);
            return DarwinResponse<CreatedCategoryResponse>.Success(entity.Adapt<CreatedCategoryResponse>(), 201);
        }
    }
}