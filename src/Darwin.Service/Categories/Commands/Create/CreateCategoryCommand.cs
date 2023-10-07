using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
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
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IGenericRepository<Category> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<CreatedCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity= request.Model.Adapt<Category>();
            await _repository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<CreatedCategoryResponse>.Success(entity.Adapt<CreatedCategoryResponse>(), 201);
        }
    }
}