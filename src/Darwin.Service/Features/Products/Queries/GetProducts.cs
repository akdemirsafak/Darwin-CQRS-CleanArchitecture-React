using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Products;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Features.Products.Queries;

public static class GetProducts
{
    public record Query() : IQuery<DarwinResponse<List<GetProductResponse>>>;

    public class QueryHandler(IGenericRepository<Product> _repository) 
        : IQueryHandler<Query, DarwinResponse<List<GetProductResponse>>>
    {

        public async Task<DarwinResponse<List<GetProductResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();

            return DarwinResponse<List<GetProductResponse>>.Success(products.Adapt<List<GetProductResponse>>(), 200);

        }
    }
}

