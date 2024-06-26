﻿using Darwin.Application.Features.Contents.Commands;
using Darwin.Application.Features.Contents.Queries;
using Darwin.Domain.RequestModels;
using Darwin.Domain.RequestModels.Contents;
using Darwin.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.Presentation.Controllers;

public class ContentController : CustomBaseController
{
    public ContentController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _mediator.Send(new GetContents.Query()));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> List([FromBody] GetPaginationListRequest request)
    {
        return CreateActionResult(await _mediator.Send(new GetContentList.Query(request)));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new GetContentById.Query(id)));
    }
    [HttpGet("Search")]
    public async Task<IActionResult> Search([FromQuery] string searchText)
    {
        return CreateActionResult(await _mediator.Send(new SearchContents.Query(searchText)));
    }
    [HttpGet("FullTextSearch")]
    public async Task<IActionResult> FullTextSearch([FromQuery] string searchText)
    {
        return CreateActionResult(await _mediator.Send(new FullTextSearchContents.Query(searchText)));
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateContentRequest request)
    {

        return CreateActionResult(await _mediator.Send(new CreateContent.Command(request)));
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateContentRequest request)
    {
        return CreateActionResult(await _mediator.Send(new UpdateContent.Command(id, request)));
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new DeleteContent.Command(id)));
    }
}
