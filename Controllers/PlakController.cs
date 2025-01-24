using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MelMusic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MelMusic.Controllers;

public class PlakController : Controller
{     private readonly MyContext _context;

    public PlakController(MyContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Plak(string sortOrder)
    {
        ViewBag.PriceSortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : "";

    var plaklar = _context.plaklar.AsQueryable();

    if (sortOrder == "asc")
    {
        plaklar = plaklar.OrderBy(g => g.fiyat); // Artan fiyat sıralaması
    }
    else if (sortOrder == "desc")
    {
        plaklar = plaklar.OrderByDescending(g => g.fiyat); // Azalan fiyat sıralaması
    }

    return View(plaklar.ToList());
        // var gitar=_context.gitarlar.ToList();
        // return View(gitar);
    }

    // public IActionResult Plak(){       // ürün listele
    //     var pl=_context.plaklar.ToList();
    //     return View(pl);
    // }
    public IActionResult Kayit(){
        return View();
    }
    [HttpPost]
    public IActionResult Kayit(Plak pl, IFormFile Resim)
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
            pl.resim = "/images/" + dosyaAdi;
        }
        if (pl != null)
        {
            _context.plaklar.Add(pl);
            _context.SaveChanges();
        }
        return RedirectToAction("Plak");
    }
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult Guncelle(Plak plak, IFormFile Resim)
    {
        var mplak = _context.plaklar.Find(plak.id);
        if (mplak != null)
        {
            mplak.isim = plak.isim;
            mplak.fiyat = plak.fiyat;
            mplak.aciklama = plak.aciklama;

            if (Resim != null && Resim.Length > 0)
            {
                var dosyaAdi = Path.GetFileName(Resim.FileName);
                var dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot/images", dosyaAdi);
                using (var stream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    Resim.CopyTo(stream);
                }
                mplak.resim = "/images/" + dosyaAdi;
            }

            _context.SaveChanges();
        }
        return RedirectToAction("Plak");
    }

    // Gitar Silme
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult Sil(int id)
    {
        var plak = _context.plaklar.Find(id);
        if (plak != null)
        {
            _context.plaklar.Remove(plak);
            _context.SaveChanges();
        }
        return RedirectToAction("Plak");
    }
}




