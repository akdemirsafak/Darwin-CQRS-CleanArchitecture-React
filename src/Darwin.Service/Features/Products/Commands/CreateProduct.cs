using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Request.Product;
using Darwin.Model.Response.Products;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Products.Commands;

public static class CreateProduct
{
    public record Command(CreateProductRequest Model) : ICommand<DarwinResponse<CreatedProductResponse>>;


    public class CommandHandler(IGenericRepository<Product> _repository) 
        : ICommandHandler<Command, DarwinResponse<CreatedProductResponse>>
    {
  
        public async Task<DarwinResponse<CreatedProductResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = request.Model.Adapt<Product>();
            await _repository.CreateAsync(entity);
            return DarwinResponse<CreatedProductResponse>.Success(entity.Adapt<CreatedProductResponse>(), 201);
        }
    }
    public class CreateProductCommandValidator : AbstractValidator<Command>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().NotNull().Length(3, 64);
            RuleFor(x => x.Model.Price).NotNull().GreaterThan(0);

        }
    }

}
