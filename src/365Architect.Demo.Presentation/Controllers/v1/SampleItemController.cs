using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365Architect.Demo.Application.Requests.SampleItems;
using _365Architect.Demo.Presentation.Abstractions;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace _365Architect.Demo.Presentation.Controllers.v1
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/samples/{sampleId}/items")]
    public class SampleItemController : ApiController
    {
        private readonly IMediator _mediator;

        public SampleItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new sample item
        /// </summary>
        [MapToApiVersion(1)]
        [HttpPost]
        public async Task<IActionResult> CreateSampleItemV1(int sampleId, [FromBody] CreateSampleItemCommand command)
        {
            command.SampleId = sampleId;
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.Error);
        }

        /// <summary>
        /// Update a sample item
        /// </summary>
        [MapToApiVersion(1)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSampleItemV1(int sampleId, int id, [FromBody] UpdateSampleItemCommand command)
        {
            command.SampleId = sampleId;
            command.Id = id;
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.Error);
        }

        /// <summary>
        /// Delete a sample item
        /// </summary>
        [MapToApiVersion(1)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSampleItemV1(int sampleId, int id)
        {
            var command = new DeleteSampleItemCommand
            {
                SampleId = sampleId,
                Id = id
            };

            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.Error);
        }

        /// <summary>
        /// Get detail of a sample item
        /// </summary>
        [MapToApiVersion(1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailSampleItemV1(int sampleId, int id)
        {
            var query = new GetDetailSampleItemQuery
            {
                SampleId = sampleId,
                Id = id
            };

            var result = await _mediator.Send(query);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.Error);
        }

        /// <summary>
        /// Get all items of a sample
        /// </summary>
        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> GetAllSampleItemsV1(int sampleId)
        {
            var query = new GetAllSampleItemQuery
            {
                SampleId = sampleId
            };

            var result = await _mediator.Send(query);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.Error);
        }
    }
}
