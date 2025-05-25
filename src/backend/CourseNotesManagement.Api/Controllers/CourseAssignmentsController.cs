using CourseNotesManagement.Application.Features.CourseAssignments.Commands.Create;
using CourseNotesManagement.Application.Features.CourseAssignments.Commands.Delete;
using CourseNotesManagement.Application.Features.CourseAssignments.Commands.Update;
using CourseNotesManagement.Application.Features.CourseAssignments.Queries.GetAllCourseAssignments;
using CourseNotesManagement.Application.Features.CourseAssignments.Queries.GetCourseAssignmentById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseNotesManagement.Api.Controllers
{
    [Authorize]
    public class CourseAssignmentsController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetCourseAssignmentByIdQuery(id));
            if (!result.Success)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllCourseAssignmentsQuery());
            if (!result.Success)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCourseAssignmentCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Value }, null);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCourseAssignmentCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id uyuşmuyor.");

            var result = await Mediator.Send(command);
            if (!result.Success)
                return NotFound(result.Error);

            return Ok(result.Message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteCourseAssignmentCommand(id));
            if (!result.Success)
                return NotFound(result.Error);

            return Ok(result.Message);
        }
    }
}
