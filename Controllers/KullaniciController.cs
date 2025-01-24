using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MelMusic.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace MelMusic.Controllers;

public class KullaniciController : Controller
{
    private readonly MyContext _context;

    public KullaniciController(MyContext context)
    {
        _context = context;
    }

    public IActionResult Kayit()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Kayit(Kullanici kullanici)
    {
        if (ModelState.IsValid)
        {
            // Veritabanında hiç kullanıcı yoksa, ilk kullanıcıyı admin yap
            if (!_context.kullanicilar.Any())
            {
                kullanici.rol = "Admin"; // İlk kullanıcı Admin olacak
            }
            else
            {
                kullanici.rol = "User"; // Diğer tüm kullanıcılar "User" olacak
            }

            // Kullanıcıyı veritabanına ekle
            _context.kullanicilar.Add(kullanici);
            _context.SaveChanges();
            TempData["Success"] = "Kayıt başarılı! Şimdi giriş yapabilirsiniz.";
            return RedirectToAction("Giris");
        }
        return View(kullanici);
        // if (ModelState.IsValid)
        // {
        //     _context.kullanicilar.Add(kullanici);
        //     _context.SaveChanges();
        //     TempData["Success"] = "Kayıt başarılı! Şimdi giriş yapabilirsiniz.";
        //     return RedirectToAction("Giris");
        // }
        // return View(kullanici);
    }

    // Giriş Sayfası
    public IActionResult Giris()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Giris(string ad, string sifre)
    {
        var user = _context.kullanicilar.FirstOrDefault(u => u.isim == ad &&
u.parola == sifre);
        if (user != null)
        {
            var claims = new List<Claim>
        {
        new Claim(ClaimTypes.Name, user.isim),
        new Claim(ClaimTypes.Role, user.rol)
        };
            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync("CookieAuth", principal);
            TempData["UserName"] = user.isim;
            if (user.rol == "Admin")
            {
                return RedirectToAction("AdminAnaSayfa", "Admin"); // Admin controller ve methodu oluşturmalısın.
            }
            return RedirectToAction("AnaSayfa", "Home");
        }
        ViewBag.Error = "Kullanıcı adı veya parola hatalı.";
        return View();
    }
    //     var user = _context.kullanicilar.FirstOrDefault(u => u.mail == email && u.parola == sifre);
    //     if (user != null)
    //     {
    //         HttpContext.Session.SetString("UserEmail", user.mail);
    //         TempData["Success"] = $"Hoş geldiniz, {user.isim}!";
    //         return RedirectToAction("AnaSayfa", "Home");
    //     }

    //     TempData["Error"] = "E-posta veya şifre hatalı!";
    //     return View();
    // }

    // Çıkış

    [Authorize]
    public IActionResult Cikis()
    {

        HttpContext.SignOutAsync("CookieAuth");
        return RedirectToAction("AnaSayfa", "Home");
        //     HttpContext.Session.Remove("UserEmail");
        //     TempData["Success"] = "Başarıyla çıkış yaptınız.";
        //     return RedirectToAction("Giris");
        // }
    }
}
