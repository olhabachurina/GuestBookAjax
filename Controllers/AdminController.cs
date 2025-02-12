﻿using GuestBookAjax.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuestBookAjax.Controllers
{
    public class AdminController : Controller
    {
        private readonly PasswordHasherScript _passwordHasherScript;

        public AdminController(PasswordHasherScript passwordHasherScript)
        {
            _passwordHasherScript = passwordHasherScript;
        }

        public async Task<IActionResult> RehashPasswords()
        {
            await _passwordHasherScript.RehashPasswords();
            return Content("Passwords rehashed successfully.");
        }
    }
}
