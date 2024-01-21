using Darwin.Model.Request.Product;
using Darwin.Service.Features.Products.Commands;
using Darwin.Service.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Darwin.API.Controllers;

public class ProductController : CustomBaseController
{
    public ProductController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _mediator.Send(new GetProducts.Query()));
    }
  

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {

        return CreateActionResult(await _mediator.Send(new CreateProduct.Command(request)));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request)
    {
        return CreateActionResult(await _mediator.Send(new UpdateProduct.Command(id, request)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new DeleteProduct.Command(id)));
    }
}
