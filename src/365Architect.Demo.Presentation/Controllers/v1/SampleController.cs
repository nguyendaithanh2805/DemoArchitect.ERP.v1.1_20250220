using _365Architect.Demo.Application.Requests.Samples;
using _365Architect.Demo.Application.UserCases.Samples;
using _365Architect.Demo.Presentation.Abstractions;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace _365Architect.Demo.Command.Presentation.Controllers.v1
{
    /// <summary>
    /// Controller version 1 for sample apis
    /// </summary>
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/samples")]
    public class SampleController : ApiController
    {
        private readonly IMediator mediator;

        public SampleController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Api version 1 for create sample
        /// </summary>
        /// <param name="command">Request to create sample</param>
        /// <returns>Action result</returns>
        [MapToApiVersion(1)]
        [HttpPost]
        public async Task<IActionResult> CreateSampleV1(CreateSampleCommand command)
        {
            var result = await mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.Error);
        }

        /// <summary>
        /// Api version 1 for update sample
        /// </summary>
        /// <param name="id">Id of sample need to be updated</param>
        /// <param name="request">Request body contains content to update</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateSampleV1(int id, [FromBody] UpdateSampleCommand request)
        {
            request.Id = id;
            var result = await mediator.Send(request);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.Error);
        }

        /// <summary>
        /// Api version 1 for delete sample
        /// </summary>
        /// <param name="id">id of sample</param>
        /// <returns>Action result</returns>
        [MapToApiVersion(1)]
        [HttpDelete]
        public async Task<IActionResult> DeleteSampleV1(int id)
        {
            var command = new DeleteSampleCommand()
            {
                Id = id,
            };
            var result = await mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.Error);
        }


        /// <summary>
        /// Api version 1 for get sample by id
        /// </summary>
        /// <param name="id">ID of sample</param>
        /// <returns>Action result with sample as data</returns>
        [MapToApiVersion(1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSampleByIdV1(int id)
        {
            var query = new GetDetailSampleQuery()
            {
                Id = id,
            };
            var result = await mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.Error);
        }

        /// <summary>
        /// Api version 1 for get all samples
        /// </summary>
        /// <returns>Action result with list of samples as data</returns>
        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> GetAllSamplesV1()
        {
            var query = new GetAllSampleQuery();
            var result = await mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.Error);
        }
    }
}