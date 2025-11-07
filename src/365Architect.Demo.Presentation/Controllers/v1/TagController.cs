using _365Architect.Demo.Application.Requests.Tags;
using _365Architect.Demo.Presentation.Abstractions;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace _365Architect.Demo.Command.Presentation.Controllers.v1
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/tags")]
    public class TagController : ApiController
    {
        private readonly IMediator mediator;

        public TagController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [MapToApiVersion(1)]
        [HttpPost]
        public async Task<IActionResult> CreateTagV1([FromBody] CreateTagCommand command)
        {
            var result = await mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTagV1(int id, [FromBody] UpdateTagCommand request)
        {
            request.Id = id;
            var result = await mediator.Send(request);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.Error);
        }

        [MapToApiVersion(1)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTagV1(int id)
        {
            var command = new DeleteTagCommand { Id = id };
            var result = await mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.Error);
        }

        [MapToApiVersion(1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagByIdV1(int id)
        {
            var query = new GetDetailTagQuery { Id = id };
            var result = await mediator.Send(query);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.Error);
        }

        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> GetAllTagsV1()
        {
            var query = new GetAllTagQuery();
            var result = await mediator.Send(query);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.Error);
        }
    }
}
