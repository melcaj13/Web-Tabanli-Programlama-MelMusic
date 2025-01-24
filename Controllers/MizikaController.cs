using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MelMusic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MelMusic.Controllers;

public class MizikaController : Controller
{        private readonly MyContext _context;

    public MizikaController(MyContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult Mizika(string sortOrder)
    {
        ViewBag.PriceSortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : "";

    var mizikalar = _context.mizikalar.AsQueryable();

    if (sortOrder == "asc")
    {
        mizikalar = mizikalar.OrderBy(g => g.fiyat); 
    }
    else if (sortOrder == "desc")
    {
        mizikalar = mizikalar.OrderByDescending(g => g.fiyat); 
    }

    return View(mizikalar.ToList());
    }

    public IActionResult Kayit(){
        return View();
    }
    [HttpPost]
    public IActionResult Kayit(Mizika m, IFormFile Resim)
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
            m.resim = "/images/" + dosyaAdi;
        }
        if (m != null)
        {
            _context.mizikalar.Add(m);
            _context.SaveChanges();
        }
        return RedirectToAction("Mizika");
    }
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult Guncelle(Mizika mizika, IFormFile Resim)
    {
        var mm = _context.mizikalar.Find(mizika.id);
        if (mm != null)
        {
            mm.isim = mizika.isim;
            mm.fiyat = mizika.fiyat;
            mm.aciklama = mizika.aciklama;

            if (Resim != null && Resim.Length > 0)
            {
                var dosyaAdi = Path.GetFileName(Resim.FileName);
                var dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot/images", dosyaAdi);
                using (var stream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    Resim.CopyTo(stream);
                }
                mizika.resim = "/images/" + dosyaAdi;
            }

            _context.SaveChanges();
        }
        return RedirectToAction("Mizika");
    }

   
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult Sil(int id)
    {
        var mizika = _context.mizikalar.Find(id);
        if (mizika != null)
        {
            _context.mizikalar.Remove(mizika);
            _context.SaveChanges();
        }
        return RedirectToAction("Mizika");
    }
}





