using Azure;
using GuestBookAjax.Data;
using GuestBookAjax.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GuestBookAjax.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var messages = await _repository.GetAllMessagesAsync();
            return View(messages);
        }

        public async Task<IActionResult> MyReviews()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _repository.GetUserByIdAsync(userId.Value);
            var messages = await _repository.GetMessagesByUserIdAsync(userId.Value);

            ViewBag.UserName = user.Name;

            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage([FromBody] MessageViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Unauthorized("Please log in to add a message.");
            }

            if (ModelState.IsValid)
            {
                var user = await _repository.GetUserByIdAsync(userId.Value);
                var newMessage = new Message
                {
                    Id_User = userId.Value,
                    Content = model.Content,
                    Email = model.Email,
                    WWW = model.WWW,
                    MessageDate = DateTime.Now,
                    User = user
                };

                await _repository.AddMessageAsync(newMessage);

                var messages = await _repository.GetAllMessagesAsync();
                return Json(messages);
            }

            return BadRequest("Invalid message data.");
        }
    }
}