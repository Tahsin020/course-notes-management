using CourseNotesManagement.Application.Features.Courses.Commands.Create;
using CourseNotesManagement.Application.Features.Courses.Commands.Delete;
using CourseNotesManagement.Application.Features.Courses.Commands.Update;
using CourseNotesManagement.Application.Features.Courses.Queries.GetAllCourses;
using CourseNotesManagement.Application.Features.Courses.Queries.GetCourseById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseNotesManagement.Api.Controllers
{
    [Authorize]
    public class CoursesController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetCourseByIdQuery(id));
            if (!result.Success)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllCoursesQuery());
            if (!result.Success)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCourseCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Value }, null);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCourseCommand command)
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
            var result = await Mediator.Send(new DeleteCourseCommand(id));
            if (!result.Success)
                return NotFound(result.Error);

            return Ok(result.Message);
        }
    }
}
