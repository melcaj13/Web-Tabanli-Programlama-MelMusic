using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MelMusic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace MelMusic.Controllers;

public class KalimbaController : Controller
{
    private readonly MyContext _context;

    public KalimbaController(MyContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult Kalimba(string sortOrder)
    {
        ViewBag.PriceSortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : "";

        var kalimbalar = _context.kalimbalar.AsQueryable();

        if (sortOrder == "asc")
        {
            kalimbalar = kalimbalar.OrderBy(g => g.fiyat); // Artan fiyat sıralaması
        }
        else if (sortOrder == "desc")
        {
            kalimbalar = kalimbalar.OrderByDescending(g => g.fiyat); // Azalan fiyat sıralaması
        }

        return View(kalimbalar.ToList());
        // var gitar=_context.gitarlar.ToList();
        // return View(gitar);
    }

    // public IActionResult Kalimba()
    // {      
    //     var k=_context.kalimbalar.ToList();
    //     return View(k);
    // }
    public IActionResult Kayit()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]

    public IActionResult Kayit(Kalimba kalimba, IFormFile Resim)
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
            kalimba.resim = "/images/" + dosyaAdi;
        }
        if (kalimba != null)
        {
            _context.kalimbalar.Add(kalimba);
            _context.SaveChanges();
        }
        return RedirectToAction("Kalimba");
    }
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult Guncelle(Kalimba kalimba, IFormFile Resim)
    {
        var mkalimba = _context.kalimbalar.Find(kalimba.id);
        if (mkalimba != null)
        {
            mkalimba.isim = kalimba.isim;
            mkalimba.fiyat = kalimba.fiyat;
            mkalimba.aciklama = kalimba.aciklama;

            if (Resim != null && Resim.Length > 0)
            {
                var dosyaAdi = Path.GetFileName(Resim.FileName);
                var dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot/images", dosyaAdi);
                using (var stream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    Resim.CopyTo(stream);
                }
                mkalimba.resim = "/images/" + dosyaAdi;
            }

            _context.SaveChanges();
        }
        return RedirectToAction("Kalimba");
    }

    // Gitar Silme
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult Sil(int id)
    {
        var kalimba = _context.kalimbalar.Find(id);
        if (kalimba != null)
        {
            _context.kalimbalar.Remove(kalimba);
            _context.SaveChanges();
        }
        return RedirectToAction("Kalimba");
    }
}


