using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Common;
using Darwin.Service.Common;
using FluentValidation;

namespace Darwin.Service.Features.Categories.Commands;

public static class DeleteCategory
{
    public record Command(Guid Id) : ICommand<DarwinResponse<NoContent>>;
    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IUnitOfWork _unitOfWork;


        public CommandHandler(IGenericRepository<Category> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existMusic = await _repository.GetAsync(x => x.Id == request.Id);
            if (existMusic == null)
                return DarwinResponse<NoContent>.Fail("");
            existMusic.IsUsable = false;
            await _repository.UpdateAsync(existMusic);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<NoContent>.Success(204);
        }
    }
    public class DeleteCategoryCommandValidator : AbstractValidator<Command>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }

}
