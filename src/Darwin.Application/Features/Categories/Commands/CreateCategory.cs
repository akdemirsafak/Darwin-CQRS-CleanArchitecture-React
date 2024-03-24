using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.Azure;
using Darwin.Domain.BaseDto;
using Darwin.Domain.RequestModels.Categories;
using Darwin.Domain.ResponseModels.Categories;
using FluentValidation;

namespace Darwin.Application.Features.Categories.Commands;

public static class CreateCategory
{
    public record Command(CreateCategoryRequest Model) : ICommand<DarwinResponse<CreatedCategoryResponse>>;


    public class CommandHandler : ICommandHandler<Command, DarwinResponse<CreatedCategoryResponse>>
    {
        private readonly ICategoryService _categoryService;
        private readonly IAzureBlobStorageService _azureBlobStorageService;
        private readonly IFileService _fileService;

        public CommandHandler(ICategoryService categoryService
            , IAzureBlobStorageService azureBlobStorageService
            , IFileService fileService)
        {
            _categoryService = categoryService;
            _azureBlobStorageService = azureBlobStorageService;
            _fileService = fileService;
        }

        public async Task<DarwinResponse<CreatedCategoryResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            //BlobResponseDto uploadResponse = await _azureBlobStorageService.UploadAsync(request.Model.ImageFile, "categoryimages");
            //string imageUrl = uploadResponse.Blob.Url;
            string imageUrl= await _fileService.UploadImage(request.Model.ImageFile);

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
