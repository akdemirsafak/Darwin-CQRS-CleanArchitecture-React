using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Azure;
using Darwin.Model.Request.Categories;
using Darwin.Model.Response.Categories;
using Darwin.Service.Common;
using Darwin.Service.Services;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Categories.Commands;

public static class CreateCategory
{
    public record Command(CreateCategoryRequest Model) : ICommand<DarwinResponse<CreatedCategoryResponse>>;


    public class CommandHandler(IGenericRepository<Category> _repository,
        IAzureBlobStorageService _azureBlobStorageService) 
        : ICommandHandler<Command, DarwinResponse<CreatedCategoryResponse>>
    {
  
        public async Task<DarwinResponse<CreatedCategoryResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = request.Model.Adapt<Category>();

            BlobResponseDto uploadResponse= await _azureBlobStorageService.UploadAsync(request.Model.ImageFile,"categoryimages");
            entity.ImageUrl = uploadResponse.Blob.Url;

            await _repository.CreateAsync(entity);
            return DarwinResponse<CreatedCategoryResponse>.Success(entity.Adapt<CreatedCategoryResponse>(), 201);
        }
    }
    public class CreateCategoryCommandValidator : AbstractValidator<Command>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().NotNull().Length(3, 64);
        }
    }

}
