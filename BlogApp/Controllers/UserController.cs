﻿using BlogApp.Context;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogApp.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index" , "Post");
            } 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);
                if (isUser != null)
                {
                    var UserClaims = new List<Claim>();

                    UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));
                    UserClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));
                    UserClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? ""));
                    UserClaims.Add(new Claim(ClaimTypes.UserData, isUser.Image ?? ""));


                    if (isUser.Email == "info@sevvalyldrm.com")
                    {
                        UserClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }

                    var claimsIdentity = new ClaimsIdentity(UserClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        //uygulama beni hatırlasın
                        IsPersistent = true,
                    };

                    //varsa sil
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties
                        );

                    return RedirectToAction("Index", "Post");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
                }
            }

            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
		}
    }
}
