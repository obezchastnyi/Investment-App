using System;
using System.Linq;
using InvestmentApp.DB;
using InvestmentApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvestmentApp.V1.Controllers;

public class BaseController : Controller
{
    private readonly InvestmentAppDbContext _context;
    private readonly IPasswordManager _passwordManager;

    public BaseController(InvestmentAppDbContext context, IPasswordManager passwordManager)
    {
        this._context = context ?? throw new ArgumentNullException(nameof(context));
        this._passwordManager = passwordManager ?? throw new ArgumentNullException(nameof(passwordManager));
    }

    [NonAction]
    protected bool Authenticate(string userName, string password)
    {
        var user = this._context.User
            .AsNoTracking()
            .FirstOrDefault(u => u.UserName == userName);

        return user != null && this._passwordManager.Verify(password, user.PasswordHash);
    }
}
