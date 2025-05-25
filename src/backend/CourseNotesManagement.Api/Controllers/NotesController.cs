using CourseNotesManagement.Application.Features.Notes.Commands.Create;
using CourseNotesManagement.Application.Features.Notes.Commands.Delete;
using CourseNotesManagement.Application.Features.Notes.Commands.Update;
using CourseNotesManagement.Application.Features.Notes.Queries.GetAllNotes;
using CourseNotesManagement.Application.Features.Notes.Queries.GetNoteById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseNotesManagement.Api.Controllers
{
    [Authorize]
    public class NotesController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetNoteByIdQuery(id));
            if (!result.Success)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllNotesQuery());
            if (!result.Success)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNoteCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Value }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateNoteCommand command)
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
            var result = await Mediator.Send(new DeleteNoteCommand(id));
            if (!result.Success)
                return NotFound(result.Error);

            return Ok(result.Message);
        }
    }
}
