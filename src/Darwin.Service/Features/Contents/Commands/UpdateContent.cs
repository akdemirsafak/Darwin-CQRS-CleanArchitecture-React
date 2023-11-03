using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.Contents;
using Darwin.Model.Response.Contents;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Contents.Commands;

public static class UpdateContent
{
    public record Command(Guid Id, UpdateContentRequest Model) : ICommand<DarwinResponse<UpdatedContentResponse>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<UpdatedContentResponse>>
    {
        private readonly IGenericRepository<Content> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(IGenericRepository<Content> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<UpdatedContentResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existContent = await _repository.GetAsync(x => x.Id == request.Id);
            if (existContent == null)
            {
                return DarwinResponse<UpdatedContentResponse>.Fail("");
            }
            existContent.ImageUrl = request.Model.ImageUrl;
            existContent.Name = request.Model.Name != existContent.Name ? request.Model.Name : existContent.Name;
            existContent.Lyrics = request.Model.Lyrics != existContent.Lyrics ? request.Model.Lyrics : existContent.Lyrics;
            existContent.IsUsable = request.Model.IsUsable;
            existContent.UpdatedAt = DateTime.UtcNow.Ticks;
            await _repository.UpdateAsync(existContent);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<UpdatedContentResponse>.Success(existContent.Adapt<UpdatedContentResponse>());
        }
    }
    public class UpdateContentCommandValidator : AbstractValidator<Command>
    {
        public UpdateContentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Model.Name).NotEmpty().NotNull().Length(3, 64);
        }
    }

}