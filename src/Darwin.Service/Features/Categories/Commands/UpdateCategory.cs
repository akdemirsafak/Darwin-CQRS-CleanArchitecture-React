using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.Categories;
using Darwin.Model.Response.Categories;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Categories.Commands;

public static class UpdateCategory
{
    public record Command(Guid Id, UpdateCategoryRequest Model) : ICommand<DarwinResponse<UpdatedCategoryResponse>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<UpdatedCategoryResponse>>
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(IGenericRepository<Category> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<UpdatedCategoryResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existCategory = await _repository.GetAsync(x => x.Id == request.Id);
            if (existCategory == null)
            {
                return DarwinResponse<UpdatedCategoryResponse>.Fail("");
            }
            existCategory.ImageUrl = request.Model.ImageUrl;
            existCategory.Name = request.Model.Name;
            existCategory.IsUsable = request.Model.IsUsable;
            existCategory.UpdatedAt = DateTime.UtcNow.Ticks;
            await _repository.UpdateAsync(existCategory);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<UpdatedCategoryResponse>.Success(existCategory.Adapt<UpdatedCategoryResponse>());
        }
    }
    public class UpdateCategoryCommandValidator : AbstractValidator<Command>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Model.Name).NotEmpty().NotNull().Length(3, 64);
        }
    }
}


