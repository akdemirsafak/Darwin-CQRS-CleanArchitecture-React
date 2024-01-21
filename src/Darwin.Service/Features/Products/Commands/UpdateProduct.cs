using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Request.Product;
using Darwin.Model.Response.Products;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Products.Commands;

public static class UpdateProduct
{
    public record Command(Guid Id, UpdateProductRequest Model) : ICommand<DarwinResponse<UpdatedProductResponse>>;

    public class CommandHandler(IGenericRepository<Product> _repository) 
        : ICommandHandler<Command, DarwinResponse<UpdatedProductResponse>>
    {
        public async Task<DarwinResponse<UpdatedProductResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existProduct = await _repository.GetAsync(x => x.Id == request.Id);
            if (existProduct == null)
                return DarwinResponse<UpdatedProductResponse>.Fail("");

            existProduct.Name = request.Model.Name;
            existProduct.Price = request.Model.Price;

            await _repository.UpdateAsync(existProduct);
            return DarwinResponse<UpdatedProductResponse>.Success(existProduct.Adapt<UpdatedProductResponse>());
        }
    }
    public class UpdateProductCommandValidator : AbstractValidator<Command>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Model.Name).NotEmpty().NotNull().Length(3, 64);
            RuleFor(x => x.Model.Price).NotNull().GreaterThan(0);
        }
    }
}


