﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDHDotNetCore.ATMMvcApp.EFDbContext;
using SDHDotNetCore.ATMMvcApp.Models;
using System.Threading.Tasks;

namespace SDHDotNetCore.ATMMvcApp.Controllers
{
    public class ATMController : Controller
    {
        private readonly AppDbContext _context;

        public ATMController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ATMUserModel model)
        {
            var user = await _context.Users
                .Where(x => x.CardNumber == model.CardNumber && x.Pin == model.Pin)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                return RedirectToAction("MainMenu", new { id = user.UserId });
            }

            ViewData["Error"] = "Invalid card number or PIN.";
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(ATMUserModel reqModel)
        {
            await _context.Users.AddAsync(reqModel);
            var result = await _context.SaveChangesAsync();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;

            MessageModel model = new MessageModel(result > 0, message);
            return Json(model);
        }


        public async Task<IActionResult> List()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public IActionResult MainMenu(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);

            if (user != null)
            {
                return View(user);
            }

            TempData["Message"] = "No data found.";
            TempData["IsSuccess"] = false;

            return RedirectToAction("Login"); 
        }

        public IActionResult Withdraw(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);

            if (user != null)
            {
                return View(user);
            }

            TempData["Message"] = "No data found.";
            TempData["IsSuccess"] = false;

            return View("MainMenu");
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(int id, ATMUserModel reqModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user is null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Json(user);
            }

            if (user.Balance < reqModel.Balance)
            {
                TempData["Message"] = "Withdrawal failed. Insufficient balance.";
                TempData["IsSuccess"] = false;
                return Json(user);
            }

            user.Balance -= reqModel.Balance;
            _context.Users.Entry(user).State = EntityState.Modified;
            int result = _context.SaveChanges();
            string message = result > 0 ? "Withdraw Successful." : "Withdraw Failed.";

            MessageModel model = new MessageModel(result > 0, message);
            return Json(model);
        }

        [HttpGet]
        public IActionResult Deposit(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);

            if (user != null)
            {
                return View(user);
            }

            TempData["Message"] = "No data found.";
            TempData["IsSuccess"] = false;

            return View("MainMenu");
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(int id, ATMUserModel reqModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user is null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Json(user);
            }

            user.Balance += reqModel.Balance;
            _context.Users.Entry(user).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();
            string message = result > 0 ? "Deposit Successful." : "Deposit Failed.";

            MessageModel model = new MessageModel(result > 0, message);
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> CheckBalance(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user is null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return RedirectToAction("MainMenu");
            }

            ViewData["Balance"] = user.Balance;

            return View(user);
        }
    }
}
