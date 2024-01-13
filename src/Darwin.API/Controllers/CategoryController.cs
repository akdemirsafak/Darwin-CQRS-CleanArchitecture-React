using Darwin.Model.Request;
using Darwin.Model.Request.Categories;
using Darwin.Service.Features.Categories.Commands;
using Darwin.Service.Features.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Darwin.API.Controllers;

public class CategoryController : CustomBaseController
{
    public CategoryController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _mediator.Send(new GetCategories.Query()));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> GetCategoryList([FromBody] GetPaginationListRequest request)
    {
        return CreateActionResult(await _mediator.Send(new GetCategoryList.Query(request)));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new GetCategoryById.Query(id)));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
    {

        return CreateActionResult(await _mediator.Send(new CreateCategory.Command(request)));
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryRequest request)
    {
        return CreateActionResult(await _mediator.Send(new UpdateCategory.Command(id, request)));
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new DeleteCategory.Command(id)));
    }
}
