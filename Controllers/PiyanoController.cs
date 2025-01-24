using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MelMusic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MelMusic.Controllers;

public class PiyanoController : Controller
{       
     private readonly MyContext _context;

    public PiyanoController(MyContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult Piyano(string sortOrder)
    {
        ViewBag.PriceSortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : "";

    var piyanolar = _context.piyanolar.AsQueryable();

    if (sortOrder == "asc")
    {
        piyanolar = piyanolar.OrderBy(g => g.fiyat); // Artan fiyat sıralaması
    }
    else if (sortOrder == "desc")
    {
        piyanolar = piyanolar.OrderByDescending(g => g.fiyat); // Azalan fiyat sıralaması
    }

    return View(piyanolar.ToList());
        // var gitar=_context.gitarlar.ToList();
        // return View(gitar);
    }

    // public IActionResult Piyano(){       
    //     var piyano=_context.piyanolar.ToList();
    //     return View(piyano);
    // }
    public IActionResult Kayit(){
        return View();
    }
    [HttpPost]
    public IActionResult Kayit(Piyano p, IFormFile Resim)
    {  // kaydet
        if (Resim != null && Resim.Length > 0)
        {
            var dosyaAdi = Path.GetFileName(Resim.FileName);
            var dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot/images", dosyaAdi);
            using (var stream = new FileStream(dosyaYolu, FileMode.Create))
            {
                Resim.CopyTo(stream);
            }
            p.resim = "/images/" + dosyaAdi;
        }
        if (p != null)
        {
            _context.piyanolar.Add(p);
            _context.SaveChanges();
        }
        return RedirectToAction("Piyano");
    }
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult Guncelle(Piyano piyano, IFormFile Resim)
    {
        var mp = _context.piyanolar.Find(piyano.id);
        if (mp != null)
        {
            mp.isim = piyano.isim;
            mp.fiyat = piyano.fiyat;
            mp.aciklama = piyano.aciklama;

            if (Resim != null && Resim.Length > 0)
            {
                var dosyaAdi = Path.GetFileName(Resim.FileName);
                var dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot/images", dosyaAdi);
                using (var stream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    Resim.CopyTo(stream);
                }
                mp.resim = "/images/" + dosyaAdi;
            }

            _context.SaveChanges();
        }
        return RedirectToAction("Piyano");
    }

    // Gitar Silme
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult Sil(int id)
    {
        var piyano = _context.piyanolar.Find(id);
        if (piyano != null)
        {
            _context.piyanolar.Remove(piyano);
            _context.SaveChanges();
        }
        return RedirectToAction("Piyano");
    }
}





