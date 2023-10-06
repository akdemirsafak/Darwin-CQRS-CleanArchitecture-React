using Darwin.Model.Request.Categories;
using Darwin.Service.Categories.Commands.Create;
using Darwin.Service.Categories.Commands.Delete;
using Darwin.Service.Categories.Commands.Update;
using Darwin.Service.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Darwin.API.Controllers;

[Authorize]
public class CategoryController : CustomBaseController
{
    public CategoryController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _mediator.Send(new GetCategoriesQuery()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new GetCategoryByIdQuery(id)));
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
    {

        return CreateActionResult(await _mediator.Send(new CreateCategoryCommand(request)));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryRequest request)
    {
        return CreateActionResult(await _mediator.Send(new UpdateCategoryCommand(id, request)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new DeleteCategoryCommand(id)));
    }
}
