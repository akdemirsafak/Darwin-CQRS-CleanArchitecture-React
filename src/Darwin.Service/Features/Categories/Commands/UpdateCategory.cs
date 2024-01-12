using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Request.Categories;
using Darwin.Model.Response.Categories;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Categories.Commands;

public static class UpdateCategory
{
    public record Command(Guid Id, UpdateCategoryRequest Model) : ICommand<DarwinResponse<UpdatedCategoryResponse>>;

    public class CommandHandler(IGenericRepository<Category> _repository) : ICommandHandler<Command, DarwinResponse<UpdatedCategoryResponse>>
    {
        public async Task<DarwinResponse<UpdatedCategoryResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existCategory = await _repository.GetAsync(x => x.Id == request.Id);
            if (existCategory == null)
                return DarwinResponse<UpdatedCategoryResponse>.Fail("");

            existCategory.ImageUrl = request.Model.ImageUrl;
            existCategory.Name = request.Model.Name;
            existCategory.IsUsable = request.Model.IsUsable;

            await _repository.UpdateAsync(existCategory);
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


