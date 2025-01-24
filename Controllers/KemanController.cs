using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MelMusic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace MelMusic.Controllers;

public class KemanController : Controller
{
    private readonly MyContext _context;

    public KemanController(MyContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult Keman(string sortOrder)
    {
        ViewBag.PriceSortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : "";

        var kemanlar = _context.kemanlar.AsQueryable();

        if (sortOrder == "asc")
        {
            kemanlar = kemanlar.OrderBy(g => g.fiyat); 
        }
        else if (sortOrder == "desc")
        {
            kemanlar = kemanlar.OrderByDescending(g => g.fiyat); 
        }

        return View(kemanlar.ToList());
        
     }

    public IActionResult Kayit()
    {
        return View();
    }
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]

    public IActionResult Kayit(Keman keman, IFormFile Resim)
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
            keman.resim = "/images/" + dosyaAdi;
        }
        if (keman != null)
        {
            _context.kemanlar.Add(keman);
            _context.SaveChanges();
        }
        
        return View();
    }
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult Guncelle(Keman keman, IFormFile Resim)
    {
        var mkeman = _context.kemanlar.Find(keman.id);
        if (mkeman != null)
        {
            mkeman.isim = keman.isim;
            mkeman.fiyat = keman.fiyat;
            mkeman.aciklama = keman.aciklama;

            if (Resim != null && Resim.Length > 0)
            {
                var dosyaAdi = Path.GetFileName(Resim.FileName);
                var dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot/images", dosyaAdi);
                using (var stream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    Resim.CopyTo(stream);
                }
                mkeman.resim = "/images/" + dosyaAdi;
            }

            _context.SaveChanges();
        }
        return RedirectToAction("Keman");
    }

    
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult Sil(int id)
    {
        var keman = _context.kemanlar.Find(id);
        if (keman != null)
        {
            _context.kemanlar.Remove(keman);
            _context.SaveChanges();
        }
        return RedirectToAction("Keman");
    }
}




