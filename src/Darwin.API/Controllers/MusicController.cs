using Darwin.Model.Request.Musics;
using Darwin.Service.Musics.Commands.Create;
using Darwin.Service.Musics.Commands.Delete;
using Darwin.Service.Musics.Commands.Update;
using Darwin.Service.Musics.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.API.Controllers
{
    public class MusicController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public MusicController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return CreateActionResult(await _mediator.Send(new GetMusicsQuery()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return CreateActionResult(await _mediator.Send(new GetMusicByIdQuery(id)));
        }
        [HttpGet("/Search{searchText}")]
        public async Task<IActionResult> Search(string searchText)
        {
            return CreateActionResult(await _mediator.Send(new SearchMusicsQuery(searchText)));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMusicRequest request)
        {

            return CreateActionResult(await _mediator.Send(new CreateMusicCommand(request)));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMusicRequest request)
        {
            return CreateActionResult(await _mediator.Send(new UpdateMusicCommand(id, request)));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CreateActionResult(await _mediator.Send(new DeleteMusicCommand(id)));
        }
    }
}
