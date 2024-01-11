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
    public record Command(CreatePlayListRequest Model, string creatorId) : ICommand<DarwinResponse<CreatedPlayListResponse>>;

    public class CommandHandler (IGenericRepository<PlayList> _playListRepository)
        : ICommandHandler<Command, DarwinResponse<CreatedPlayListResponse>>
    {
      

        public async Task<DarwinResponse<CreatedPlayListResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity= request.Model.Adapt<PlayList>();
            entity.CreatorId = request.creatorId;
            await _playListRepository.CreateAsync(entity);

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
