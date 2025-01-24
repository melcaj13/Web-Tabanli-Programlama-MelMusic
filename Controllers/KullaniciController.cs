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
            if (!_context.kullanicilar.Any())
            {
                kullanici.rol = "Admin"; 
            }
            else
            {
                kullanici.rol = "User"; 
            }

            _context.kullanicilar.Add(kullanici);
            _context.SaveChanges();
            TempData["Success"] = "Kayıt başarılı! Şimdi giriş yapabilirsiniz.";
            return RedirectToAction("Giris");
        }
        return View(kullanici);
    }

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
                return RedirectToAction("AdminAnaSayfa", "Admin"); 
            }
            return RedirectToAction("AnaSayfa", "Home");
        }
        ViewBag.Error = "Kullanıcı adı veya parola hatalı.";
        return View();
    }

    [Authorize]
    public IActionResult Cikis()
    {

        HttpContext.SignOutAsync("CookieAuth");
        return RedirectToAction("AnaSayfa", "Home");
    }
}
