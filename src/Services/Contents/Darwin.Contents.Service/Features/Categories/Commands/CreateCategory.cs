using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Core.RequestModels.Categories;
using Darwin.Contents.Service.Common;
using Darwin.Shared.Dtos;
using Darwin.Shared.Dtos.Azure;
using Darwin.Shared.Utils;
using FluentValidation;

namespace Darwin.Contents.Service.Features.Categories.Commands;

public static class CreateCategory
{
    public record Command(CreateCategoryRequest Model) : ICommand<DarwinResponse<NoContentDto>>;


    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContentDto>>
    {
        private readonly IFileService _fileService;
        private readonly ICategoryService _categoryService;

        public CommandHandler(
            IFileService fileService,
            ICategoryService categoryService)
        {
            _fileService = fileService;
            _categoryService = categoryService;
        }

        public async Task<DarwinResponse<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var uploadResponse=await _fileService.UploadAsync(request.Model.ImageFile, AzureContainerNames.Categories);
            BlobResponseDto blobResponseDto = uploadResponse.Data;
            string imageUrl = blobResponseDto.Blob.Url!;


            await _categoryService.CreateAsync(request.Model, imageUrl);
            return DarwinResponse<NoContentDto>.Success(201);
        }
    }
    public class CreateCategoryCommandValidator : AbstractValidator<Command>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().NotNull().Length(3, 32);
        }
    }
}
