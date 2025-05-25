using CourseNotesManagement.Application.Features.Admins.Commands.Create;
using CourseNotesManagement.Application.Features.Admins.Commands.Delete;
using CourseNotesManagement.Application.Features.Admins.Commands.Update;
using CourseNotesManagement.Application.Features.Admins.Queries.GetAdminById;
using CourseNotesManagement.Application.Features.Admins.Queries.GetAllAdmins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseNotesManagement.Api.Controllers
{
    [Authorize]
    public class AdminsController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetAdminByIdQuery(id));
            if (!result.Success)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllAdminsQuery());
            if (!result.Success)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdminCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Value }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAdminCommand command)
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
            var result = await Mediator.Send(new DeleteAdminCommand(id));
            if (!result.Success)
                return NotFound(result.Error);

            return Ok(result.Message);
        }
    }
}
