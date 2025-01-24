using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MelMusic.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace MelMusic.Controllers;

public class AdminController : Controller
{
    private readonly MyContext _context;

    public AdminController(MyContext context)
    {
        _context = context;
    }
    [Authorize(Roles = "Admin")]
    public IActionResult AdminAnaSayfa()
    {
        return View(); 
    }
}