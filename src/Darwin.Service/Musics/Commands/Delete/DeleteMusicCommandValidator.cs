using FluentValidation;

namespace Darwin.Service.Musics.Commands.Delete;

public class DeleteMusicCommandValidator : AbstractValidator<DeleteMusicCommand>
{
    public DeleteMusicCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
    }
}
