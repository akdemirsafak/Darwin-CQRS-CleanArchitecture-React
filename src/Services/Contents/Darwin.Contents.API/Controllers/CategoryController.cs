using Darwin.Contents.Core.RequestModels;
using Darwin.Contents.Core.RequestModels.Categories;
using Darwin.Contents.Service.Features.Categories.Commands;
using Darwin.Contents.Service.Features.Categories.Queries;
using Darwin.Shared.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Darwin.Contents.API.Controllers;

public class CategoryController : CustomBaseController
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _mediator.Send(new GetCategories.Query()));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> List([FromBody] GetPaginationListRequest request)
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
    public async Task<IActionResult> Create([FromForm] CreateCategoryRequest request)
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
