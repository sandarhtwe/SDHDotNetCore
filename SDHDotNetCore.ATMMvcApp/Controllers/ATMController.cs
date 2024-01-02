using Microsoft.AspNetCore.Mvc;
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
                .Where(u => u.CardNumber == model.CardNumber && u.Pin == model.Pin)
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
        public async Task<IActionResult> Register(ATMUserModel userModel)
        {
            await _context.Users.AddAsync(userModel);
            var result = await _context.SaveChangesAsync();
            var message = result > 0 ? "Registration Successful." : "Registration failed.";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;

            return RedirectToAction("Login", userModel);
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

        [HttpGet]
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
        public async Task<IActionResult> Withdraw(int id, double amount)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user is null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return View("MainMenu",user);
            }

            if (user.Balance < amount)
            {
                TempData["Message"] = "Withdrawal failed. Invalid data or insufficient balance.";
                TempData["IsSuccess"] = false;
                return View("MainMenu",user);
            }

            user.Balance -= amount;
            var result = _context.SaveChanges();
            var message = result > 0 ? $"Withdraw Successful. Remaining Balance: {user.Balance} $" : "Withdraw failed.";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;

            return View("MainMenu",user);
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
        public async Task<IActionResult> Deposit(int id, double amount)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user is null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return View("MainMenu");
            }

            user.Balance += amount;
            var result = await _context.SaveChangesAsync();
            string message = result > 0 ? "Deposit Successful." : "Deposit Failed.";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;

            return View("MainMenu",user);
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
