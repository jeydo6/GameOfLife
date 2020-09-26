using GameOfLife.Application.Commands;
using GameOfLife.Application.Dto;
using GameOfLife.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GameOfLife.WebAPI.Controllers
{
    [ApiController]
    [Route("fields")]
    public class FieldsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FieldsController> _logger;

        public FieldsController(
            IMediator mediator,
            ILogger<FieldsController> logger
        )
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Post-method
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Produces(typeof(Guid))]
        public async Task<IActionResult> Add(UInt16 size, Byte density)
        {
            var result = await _mediator.Send(new AddFieldCommand(size, density));

            return Ok(result);
        }

        /// <summary>
        /// Get-method
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces(typeof(FieldDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetFieldQuery(id));

            if (result != null)
            {
                return Ok(result);
            }

            return NoContent();
        }

        /// <summary>
        /// Get-method
        /// </summary>
        /// <returns></returns>
        [HttpGet("next/{id}")]
        [Produces(typeof(FieldDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Next(Guid id)
        {
            var result = await _mediator.Send(new NextFieldQuery(id));

            if (result != null)
            {
                return Ok(result);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete-method
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new RemoveFieldQuery(id));

            return NoContent();
        }
    }
}
