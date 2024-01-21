using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Common;
using Darwin.Service.Common;
using FluentValidation;

namespace Darwin.Service.Features.Products.Commands;

public static class DeleteProduct
{
    public record Command(Guid Id) : ICommand<DarwinResponse<NoContent>>;
    public class CommandHandler(IGenericRepository<Product> _repository) 
        : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existProduct = await _repository.GetAsync(x => x.Id == request.Id);
            if (existProduct == null)
                return DarwinResponse<NoContent>.Fail("");

            await _repository.RemoveAsync(existProduct);

            return DarwinResponse<NoContent>.Success(204);
        }
    }
    public class DeleteProductCommandValidator : AbstractValidator<Command>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }

}
