﻿using Mc2.CrudTest.Application.Basic.Enums;
using Mc2.CrudTest.Application.Models.Commands;
using Microsoft.AspNetCore.Mvc;
using MediatR;
namespace Mc2.CrudTest.Api.Controllers;


[ApiController]
[ApiVersion("1.0")]
public class CustomerController : Controller
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        this._mediator = mediator;
    }


    [Route("v{version:apiVersion}/[controller]/[action]")]
    [HttpPost]
    public async Task< IActionResult> Create(CreateCustomerCommand request)
    {
        var result = await _mediator.Send(request);

        if (result.ResponseType != ResponseType.Success)
            return BadRequest(result);

        return Ok(result);
    }
}