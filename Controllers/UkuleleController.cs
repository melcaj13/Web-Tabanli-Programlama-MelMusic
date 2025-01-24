using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MelMusic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MelMusic.Controllers;

public class UkuleleController : Controller
{     private readonly MyContext _context;

    public UkuleleController(MyContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult Ukulele(string sortOrder)
    {
        ViewBag.PriceSortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : "";

    var ukuleleler = _context.ukuleleler.AsQueryable();

    if (sortOrder == "asc")
    {
        ukuleleler = ukuleleler.OrderBy(g => g.fiyat); 
    }
    else if (sortOrder == "desc")
    {
        ukuleleler = ukuleleler.OrderByDescending(g => g.fiyat); 
    }

    return View(ukuleleler.ToList());
    }

    public IActionResult Kayit(){
        return View();
    }
    [HttpPost]
    public IActionResult Kayit(Ukulele u, IFormFile Resim)
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
            u.resim = "/images/" + dosyaAdi;
        }
        if (u != null)
        {
            _context.ukuleleler.Add(u);
            _context.SaveChanges();
        }
        return RedirectToAction("Ukulele");
    }
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult Guncelle(Ukulele ukulele, IFormFile Resim)
    {
        var mu = _context.ukuleleler.Find(ukulele.id);
        if (mu != null)
        {
            mu.isim = ukulele.isim;
            mu.fiyat = ukulele.fiyat;
            mu.aciklama = ukulele.aciklama;

            if (Resim != null && Resim.Length > 0)
            {
                var dosyaAdi = Path.GetFileName(Resim.FileName);
                var dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot/images", dosyaAdi);
                using (var stream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    Resim.CopyTo(stream);
                }
                mu.resim = "/images/" + dosyaAdi;
            }

            _context.SaveChanges();
        }
        return RedirectToAction("Ukulele");
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult Sil(int id)
    {
        var ukulele = _context.ukuleleler.Find(id);
        if (ukulele != null)
        {
            _context.ukuleleler.Remove(ukulele);
            _context.SaveChanges();
        }
        return RedirectToAction("ukulele");
    }
}





