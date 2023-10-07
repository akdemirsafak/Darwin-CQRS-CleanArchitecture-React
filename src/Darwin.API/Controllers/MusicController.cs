using Darwin.Model.Request.Musics;
using Darwin.Service.Features.Musics.Commands;
using Darwin.Service.Features.Musics.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.API.Controllers
{
    public class MusicController : CustomBaseController
    {
        public MusicController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return CreateActionResult(await _mediator.Send(new GetMusics.Query()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return CreateActionResult(await _mediator.Send(new GetMusicById.Query(id)));
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery]string searchText)
        {
            return CreateActionResult(await _mediator.Send(new SearchMusics.Query(searchText)));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMusicRequest request)
        {

            return CreateActionResult(await _mediator.Send(new CreateMusic.Command(request)));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMusicRequest request)
        {
            return CreateActionResult(await _mediator.Send(new UpdateMusic.Command(id, request)));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CreateActionResult(await _mediator.Send(new DeleteMusic.Command(id)));
        }
    }
}
