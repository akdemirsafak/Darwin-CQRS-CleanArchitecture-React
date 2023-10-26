using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.PlayLists;
using Darwin.Model.Response.PlayLists;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.PlayLists.Commands;

public static class CreatePlayList
{
    public record Command(CreatePlayListRequest Model) : ICommand<DarwinResponse<CreatedPlayListResponse>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<CreatedPlayListResponse>>
    {
        private readonly IPlayListRepository _playListRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(IPlayListRepository playListRepository, IUnitOfWork unitOfWork)
        {
            _playListRepository = playListRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<CreatedPlayListResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity= request.Model.Adapt<PlayList>();

            await _playListRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();

            return DarwinResponse<CreatedPlayListResponse>.Success(entity.Adapt<CreatedPlayListResponse>(), 201);
        }
    }
    public class CreateListValidator : AbstractValidator<Command>
    {
        public CreateListValidator()
        {
            RuleFor(x => x.Model.Name)
                .NotNull()
                .NotEmpty()
                .Length(3, 64);
        }
    }
}
