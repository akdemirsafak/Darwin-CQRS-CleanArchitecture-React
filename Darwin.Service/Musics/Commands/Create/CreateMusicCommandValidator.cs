using FluentValidation;

namespace Darwin.Service.Musics.Commands.Create;

public class UpdateMusicCommandValidator : AbstractValidator<CreateMusicCommand>
{
    public UpdateMusicCommandValidator()
    {
        RuleFor(x=>x.Model.Name).NotEmpty().NotNull().Length(3,64);
        RuleFor(x=>x.Model.Publishers).NotEmpty().NotNull().Length(3,100);

    }
}
