using FluentValidation;

namespace Darwin.Service.Musics.Queries;

public class SearchMusicsQueryValidator:AbstractValidator<SearchMusicsQuery>
{
    public SearchMusicsQueryValidator()
    {
        RuleFor(x => x.SearchText)
            .NotNull()
            .NotEmpty()
            .Length(3, 25);
    }
}
