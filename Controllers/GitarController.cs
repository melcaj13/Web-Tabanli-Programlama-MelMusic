using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MelMusic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MelMusic.Controllers;

public class GitarController : Controller
{
    private readonly MyContext _context;

    public GitarController(MyContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Gitar(string sortOrder)
    {
        ViewBag.PriceSortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : "";

        var gitarlar = _context.gitarlar.AsQueryable();

        if (sortOrder == "asc")
        {
            gitarlar = gitarlar.OrderBy(g => g.fiyat); 
        }
        else if (sortOrder == "desc")
        {
            gitarlar = gitarlar.OrderByDescending(g => g.fiyat); 
        }

        return View(gitarlar.ToList());
    }

    public IActionResult Kayit()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult Kayit(Gitar gitar, IFormFile Resim)
    {  
        if (Resim != null && Resim.Length > 0)
        {
            var dosyaAdi = Path.GetFileName(Resim.FileName);
            var dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot/images", dosyaAdi);
            using (var stream = new FileStream(dosyaYolu, FileMode.Create))
            {
                Resim.CopyTo(stream);
            }
            gitar.resim = "/images/" + dosyaAdi;
        }
        if (gitar != null)
        {
            _context.gitarlar.Add(gitar);
            _context.SaveChanges();
        }
       return View();
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult Guncelle(Gitar gitar, IFormFile Resim)
    {
        var mevcutGitar = _context.gitarlar.Find(gitar.id);
        if (mevcutGitar != null)
        {
            mevcutGitar.isim = gitar.isim;
            mevcutGitar.fiyat = gitar.fiyat;
            mevcutGitar.kategori = gitar.kategori;
            mevcutGitar.aciklama = gitar.aciklama;

            if (Resim != null && Resim.Length > 0)
            {
                var dosyaAdi = Path.GetFileName(Resim.FileName);
                var dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot/images", dosyaAdi);
                using (var stream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    Resim.CopyTo(stream);
                }
                mevcutGitar.resim = "/images/" + dosyaAdi;
            }

            _context.SaveChanges();
        }
        return RedirectToAction("Gitar");
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult Sil(int id)
    {
        var gitar = _context.gitarlar.Find(id);
        if (gitar != null)
        {
            _context.gitarlar.Remove(gitar);
            _context.SaveChanges();
        }
        return RedirectToAction("Gitar");
    }
}
