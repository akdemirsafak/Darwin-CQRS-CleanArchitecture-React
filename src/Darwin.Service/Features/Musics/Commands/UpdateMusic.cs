using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.Musics;
using Darwin.Model.Response.Musics;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Musics.Commands;

public static class UpdateMusic
{
    public record Command(Guid Id, UpdateMusicRequest Model) : ICommand<DarwinResponse<UpdatedMusicResponse>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<UpdatedMusicResponse>>
    {
        private readonly IGenericRepository<Music> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(IGenericRepository<Music> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<UpdatedMusicResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existMusic = await _repository.GetAsync(x => x.Id == request.Id);
            if (existMusic == null)
            {
                return DarwinResponse<UpdatedMusicResponse>.Fail("");
            }
            existMusic.ImageUrl = request.Model.ImageUrl;
            existMusic.Name = request.Model.Name != existMusic.Name ? request.Model.Name : existMusic.Name;
            existMusic.IsUsable = request.Model.IsUsable;
            existMusic.UpdatedAt = DateTime.UtcNow.Ticks;
            await _repository.UpdateAsync(existMusic);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<UpdatedMusicResponse>.Success(existMusic.Adapt<UpdatedMusicResponse>(), 204);
        }
    }
    public class UpdateMusicCommandValidator : AbstractValidator<Command>
    {
        public UpdateMusicCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Model.Name).NotEmpty().NotNull().Length(3, 64);
        }
    }

}