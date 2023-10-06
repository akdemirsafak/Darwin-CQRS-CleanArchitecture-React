using FluentValidation;

namespace Darwin.Service.Musics.Queries;

public class GetMusicByIdQueryValidator : AbstractValidator<GetMusicByIdQuery>
{
    public GetMusicByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotNull();
    }
}
