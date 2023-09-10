﻿using System;
using System.Collections.Generic;
using System.Linq;
using InvestmentApp.Attributes;
using InvestmentApp.DB;
using InvestmentApp.Entities;
using InvestmentApp.Interfaces;
using InvestmentApp.Models.Authentication;
using InvestmentApp.V1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InvestmentApp.V1.Controllers;

[V1]
[Route("v{version:apiVersion}/user")]
[ApiController]
public class UserController : BaseController
{
    private readonly InvestmentAppDbContext _context;
    private readonly ILogger<UserController> _logger;
    private readonly IPasswordManager _passwordManager;

    public UserController(
        ILogger<UserController> logger, InvestmentAppDbContext context, IPasswordManager passwordManager)
        : base(context, passwordManager)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._context = context ?? throw new ArgumentNullException(nameof(context));
        this._passwordManager = passwordManager ?? throw new ArgumentNullException(nameof(passwordManager));
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetAllUsers()
    {
        if (this._context.User != null)
        {
            return this.Ok(this._context.User.AsNoTracking());
        }

        this._logger.LogError($"{nameof(InvestmentApp.Models.Authentication.User)} table is empty.");
        return this.NotFound();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetUser(Guid id)
    {
        if (this._context.User != null)
        {
            var user = this._context.User.SingleOrDefault(u => u.Id == id);
            if (user != null)
            {
                return this.Ok(user);
            }

            this._logger.LogError($"{nameof(InvestmentApp.Models.Authentication.User)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(InvestmentApp.Models.Authentication.User)} table is empty.");
        return this.NotFound();
    }

    [HttpGet("auth")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult AuthenticateUser([FromQuery] UserAuthDto user)
    {
        if (this.Authenticate(user.UserName, user.Password))
        {
            var foundUser = this._context.User
                .Include(u => u.UserRole)
                .AsNoTracking()
                .SingleOrDefault(u => u.UserName == user.UserName);

            return this.Ok(foundUser.UserRole?.Code);
        }

        this._logger.LogError($"{nameof(InvestmentApp.Models.Authentication.User)} not found.");
        return this.NotFound();
    }

    [HttpPut("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateUser([FromBody] UserUpdateDto userDto)
    {
        if (userDto.Id == default)
        {
            return this.BadRequest();
        }

        var user = this._context.User.SingleOrDefault(u => u.Id == userDto.Id);
        if (user == null)
        {
            return this.NotFound();
        }

        if (!string.IsNullOrEmpty(userDto.UserName))
        {
            user.UserName = userDto.UserName;
        }

        if (userDto.RoleId.HasValue)
        {
            var role = this._context.UserRole.SingleOrDefault(u => u.Id == userDto.RoleId);
            if (role != null)
            {
                user.UserRole = role;
                user.UserRoleId = role.Id;
            }
        }

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult CreateUser([FromBody] UserDto user)
    {
        var role = this._context.UserRole.SingleOrDefault(u => u.Id == user.RoleId);

        this._context.User.Add(new User
        {
            UserName = user.UserName,
            PasswordHash = this._passwordManager.Hash(user.Password),
            UserRole = role ?? this._context.UserRole.Single(r => r.Code == UserRoles.Reader.ToString()),
            UserRoleId = role.Id
        });

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpGet("generate-hash")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    public IActionResult GenerateHash([FromQuery] string passcode)
    {
        return this.Ok(this._passwordManager.Hash(passcode));
    }

    [HttpGet("verify-hash")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    public IActionResult VerifyHash([FromQuery] string passcode, [FromQuery] string hash)
    {
        return this.Ok(this._passwordManager.Verify(passcode, hash));
    }
}
