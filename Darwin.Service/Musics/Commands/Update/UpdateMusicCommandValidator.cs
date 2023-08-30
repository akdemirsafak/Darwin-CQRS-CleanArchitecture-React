using FluentValidation;

namespace Darwin.Service.Musics.Commands.Update;

public class UpdateMusicCommandValidator : AbstractValidator<UpdateMusicCommand>
{
    public UpdateMusicCommandValidator()
    {
        RuleFor(x=>x.Id).NotEmpty().NotNull();
        RuleFor(x => x.Model.Name).NotEmpty().NotNull().Length(3, 64);
    }
}
