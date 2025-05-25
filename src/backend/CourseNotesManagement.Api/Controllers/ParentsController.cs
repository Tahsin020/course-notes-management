using CourseNotesManagement.Application.Features.Parents.Commands.Create;
using CourseNotesManagement.Application.Features.Parents.Commands.Delete;
using CourseNotesManagement.Application.Features.Parents.Commands.Update;
using CourseNotesManagement.Application.Features.Parents.Queries.GetAllParents;
using CourseNotesManagement.Application.Features.Parents.Queries.GetParentById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseNotesManagement.Api.Controllers
{
    [Authorize]
    public class ParentsController : BaseApiController
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetParentByIdQuery(id));
            if (!result.Success)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllParentsQuery());
            if (!result.Success)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateParentCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Value }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateParentCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id uyuşmuyor.");

            var result = await Mediator.Send(command);
            if (!result.Success)
                return NotFound(result.Error);

            return Ok(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteParentCommand(id));
            if (!result.Success)
                return NotFound(result.Error);

            return Ok(result.Message);
        }
    }
}
