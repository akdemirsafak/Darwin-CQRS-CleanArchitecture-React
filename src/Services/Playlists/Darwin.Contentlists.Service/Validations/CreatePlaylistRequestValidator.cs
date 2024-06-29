using Darwin.Contentlists.Core.Dtos;
using FluentValidation;

namespace Darwin.Contentlists.Service.Validations;

public sealed class CreatePlaylistRequestValidator:AbstractValidator<CreatePlaylistRequest>
{
    public CreatePlaylistRequestValidator()
    {
        RuleFor(x => x.name).NotEmpty().MaximumLength(32);
        RuleFor(x => x.description).MaximumLength(250);
    }
}