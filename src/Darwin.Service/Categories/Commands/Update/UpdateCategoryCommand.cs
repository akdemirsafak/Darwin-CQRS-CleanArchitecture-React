using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Request.Categories;
using Darwin.Model.Response.Categories;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Categories.Commands.Update;

public class UpdateCategoryCommand : ICommand<DarwinResponse<UpdatedCategoryResponse>>
{
    public Guid Id { get; }
    public UpdateCategoryRequest Model { get; }
    public UpdateCategoryCommand(Guid id, UpdateCategoryRequest model)
    {
        Id = id;
        Model = model;
    }

    public class Handler : ICommandHandler<UpdateCategoryCommand, DarwinResponse<UpdatedCategoryResponse>>
    {
        private readonly IGenericRepository<Category> _repository;


        public Handler(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<DarwinResponse<UpdatedCategoryResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existCategory= await _repository.GetAsync(x=>x.Id==request.Id);
            if (existCategory == null)
            {
                return DarwinResponse<UpdatedCategoryResponse>.Fail("");
            }
            existCategory.ImageUrl = request.Model.ImageUrl;
            existCategory.Name = request.Model.Name;
            existCategory.IsUsable = request.Model.IsUsable;
            existCategory.UpdatedAt = DateTime.UtcNow.Ticks;
            await _repository.UpdateAsync(existCategory);
            return DarwinResponse<UpdatedCategoryResponse>.Success(existCategory.Adapt<UpdatedCategoryResponse>(), 204);
        }
    }
}
