using CourseNotesManagement.Application.Features.Teachers.Commands.Create;
using CourseNotesManagement.Application.Features.Teachers.Commands.Delete;
using CourseNotesManagement.Application.Features.Teachers.Commands.Update;
using CourseNotesManagement.Application.Features.Teachers.Queries.GetAllTeachers;
using CourseNotesManagement.Application.Features.Teachers.Queries.GetTeacherById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseNotesManagement.Api.Controllers;

[Authorize] // Kullanıcı doğrulaması zorunlu
public class TeachersController : BaseApiController
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await Mediator.Send(new GetTeacherByIdQuery(id));
        if (!result.Success)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await Mediator.Send(new GetAllTeachersQuery());
        if (!result.Success)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")] // Sadece Admin ekleyebilir
    public async Task<IActionResult> Create([FromBody] CreateTeacherCommand command)
    {
        var result = await Mediator.Send(command);
        if (!result.Success)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetById), new { id = result.Value }, null);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")] // Sadece Admin güncelleyebilir
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTeacherCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id uyuşmuyor.");

        var result = await Mediator.Send(command);
        if (!result.Success)
            return NotFound(result.Error);

        return Ok(result.Message);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")] // Sadece Admin silebilir
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteTeacherCommand(id));
        if (!result.Success)
            return NotFound(result.Error);

        return Ok(result.Message);
    }
}