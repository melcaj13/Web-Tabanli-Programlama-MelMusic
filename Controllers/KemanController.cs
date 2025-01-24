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
            kemanlar = kemanlar.OrderBy(g => g.fiyat); // Artan fiyat sıralaması
        }
        else if (sortOrder == "desc")
        {
            kemanlar = kemanlar.OrderByDescending(g => g.fiyat); // Azalan fiyat sıralaması
        }

        return View(kemanlar.ToList());
        // var gitar=_context.gitarlar.ToList();
        // return View(gitar);
    }

    // public IActionResult Keman(){       // ürün listele
    //     var keman=_context.kemanlar.ToList();
    //     return View(keman);
    // }
    public IActionResult Kayit()
    {
        return View();
    }
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]

    public IActionResult Kayit(Keman keman, IFormFile Resim)
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
            keman.resim = "/images/" + dosyaAdi;
        }
        if (keman != null)
        {
            _context.kemanlar.Add(keman);
            _context.SaveChanges();
        }
        //return RedirectToAction("Keman");
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

    // Gitar Silme
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




