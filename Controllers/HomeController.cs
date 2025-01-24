using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MelMusic.Models;
using Microsoft.EntityFrameworkCore;
using MelMusic.Migrations;

namespace MelMusic.Controllers;

public class HomeController : Controller
{
    private readonly MyContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult AnaSayfa()
    {
        var viewModel = new TumUrunlerViewModel
        {
            Gitarlar = _context.gitarlar.ToList(),
            Ukuleleler = _context.ukuleleler.ToList(),
        };

        return View(viewModel);
    }


    public IActionResult Hakkimizda()
    {
        return View();
    }
    [HttpGet("TumUrunler/{sortOrder?}")]
    public IActionResult TumUrunler(string sortOrder)
{
    var viewModel = new TumUrunlerViewModel();

    if (sortOrder == "asc")
    {
        viewModel.Gitarlar = _context.gitarlar.OrderBy(g => g.fiyat).ToList();
        viewModel.Kemanlar = _context.kemanlar.OrderBy(k => k.fiyat).ToList();
        viewModel.Piyanolar = _context.piyanolar.OrderBy(p => p.fiyat).ToList();
        viewModel.Kalimbalar = _context.kalimbalar.OrderBy(k => k.fiyat).ToList();
        viewModel.Ukuleleler = _context.ukuleleler.OrderBy(u => u.fiyat).ToList();
        viewModel.Mizikalar = _context.mizikalar.OrderBy(m => m.fiyat).ToList();
        viewModel.Plaklar = _context.plaklar.OrderBy(p => p.fiyat).ToList();
    }
    else if (sortOrder == "desc")
    {
        viewModel.Gitarlar = _context.gitarlar.OrderByDescending(g => g.fiyat).ToList();
        viewModel.Kemanlar = _context.kemanlar.OrderByDescending(k => k.fiyat).ToList();
        viewModel.Piyanolar = _context.piyanolar.OrderByDescending(p => p.fiyat).ToList();
        viewModel.Kalimbalar = _context.kalimbalar.OrderByDescending(k => k.fiyat).ToList();
        viewModel.Ukuleleler = _context.ukuleleler.OrderByDescending(u => u.fiyat).ToList();
        viewModel.Mizikalar = _context.mizikalar.OrderByDescending(m => m.fiyat).ToList();
        viewModel.Plaklar = _context.plaklar.OrderByDescending(p => p.fiyat).ToList();
    }
    else
    {
        viewModel.Gitarlar = _context.gitarlar.ToList();
        viewModel.Kemanlar = _context.kemanlar.ToList();
        viewModel.Piyanolar = _context.piyanolar.ToList();
        viewModel.Kalimbalar = _context.kalimbalar.ToList();
        viewModel.Ukuleleler = _context.ukuleleler.ToList();
        viewModel.Mizikalar = _context.mizikalar.ToList();
        viewModel.Plaklar = _context.plaklar.ToList();
    }

    return View(viewModel);
}


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
