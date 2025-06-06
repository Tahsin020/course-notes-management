﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseNotesManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

    // Ortak log, hata yönetimi, UserId çekme metotları gibi eklenebilir
}