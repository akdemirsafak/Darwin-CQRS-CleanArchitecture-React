using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.RequestModels.Categories;
using Darwin.Domain.ResponseModels.Categories;
using Darwin.Share.Dtos;
using Darwin.Shared.Dtos.Azure;
using Darwin.Shared.Utils;
using FluentValidation;

namespace Darwin.Application.Features.Categories.Commands;

public static class CreateCategory
{
    public record Command(CreateCategoryRequest Model) : ICommand<DarwinResponse<CreatedCategoryResponse>>;


    public class CommandHandler : ICommandHandler<Command, DarwinResponse<CreatedCategoryResponse>>
    {
        private readonly ICategoryService _categoryService;
        private readonly IAzureBlobStorageService _azureBlobStorageService;

        public CommandHandler(ICategoryService categoryService,
            IAzureBlobStorageService azureBlobStorageService
           )
        {
            _categoryService = categoryService;
            _azureBlobStorageService = azureBlobStorageService;
        }

        public async Task<DarwinResponse<CreatedCategoryResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var uploadResponse=await _azureBlobStorageService.UploadAsync(request.Model.ImageFile, AzureContainerNames.Categories);
            BlobResponseDto blobResponseDto = uploadResponse.Data;
            string imageUrl = blobResponseDto.Blob.Url;
            //string imageUrl= await _fileService.UploadImage(request.Model.ImageFile);

            var createdCategoryResponse = await _categoryService.CreateAsync(request.Model, imageUrl);
            return DarwinResponse<CreatedCategoryResponse>.Success(createdCategoryResponse, 201);
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
