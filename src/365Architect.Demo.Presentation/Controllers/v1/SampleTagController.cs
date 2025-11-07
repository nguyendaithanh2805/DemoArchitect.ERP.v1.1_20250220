using _365Architect.Demo.Application.Requests.SampleTags;
using _365Architect.Demo.Contract.Shared;
using _365Architect.Demo.Presentation.Abstractions;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace _365Architect.Demo.Command.Presentation.Controllers.v1
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/sample-tags")]
    public class SampleTagController : ApiController
    {
        private readonly IMediator _mediator;

        public SampleTagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [MapToApiVersion(1)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSampleTagCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [MapToApiVersion(1)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSampleTagCommand request)
        {
            request.Id = id;
            var result = await _mediator.Send(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [MapToApiVersion(1)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteSampleTagCommand { Id = id };
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [MapToApiVersion(1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetDetailSampleTagQuery { Id = id };
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result) : NotFound(result.Error);
        }

        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllSampleTagQuery();
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
    }
}
