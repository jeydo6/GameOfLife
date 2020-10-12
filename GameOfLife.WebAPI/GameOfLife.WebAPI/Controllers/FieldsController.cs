using GameOfLife.Application.Commands;
using GameOfLife.Application.Dto;
using GameOfLife.Application.Queries;
using GameOfLife.Domain.Enumerations;
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
        /// Create a new field
        /// </summary>
        /// <param name="dto">DTO</param>
        /// <returns>Identifier of the created field</returns>
        [HttpPost]
        [Produces(typeof(Guid))]
        public async Task<IActionResult> Add(AddFieldDto dto)
        {
            var result = await _mediator.Send(new AddFieldCommand(dto.Size, dto.Density, dto.BehaviorEnum));

            return Ok(result);
        }

        /// <summary>
        /// Get the current field
        /// </summary>
        /// <param name="id">Identifier of the field</param>
        /// <returns>The current field object</returns>
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
        /// Get the next field
        /// </summary>
        /// <param name="id">Identifier of the field</param>
        /// <returns>The next field object</returns>
        [HttpGet("next/{id}")]
        [Produces(typeof(FieldDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Next(Guid id)
        {
            await _mediator.Send(new NextFieldCommand(id));

            return NoContent();
        }

        /// <summary>
        /// Remove the field
        /// </summary>
        /// <param name="id">Identifier of the field</param>
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
