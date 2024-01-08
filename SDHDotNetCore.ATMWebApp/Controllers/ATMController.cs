using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; 
using SDHDotNetCore.ATMWebApp.EFDbContext;
using SDHDotNetCore.ATMWebApp.Models;

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
                HttpContext.Session.SetInt32("UserId", user.UserId); 
                return RedirectToAction("MainMenu");
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
        public IActionResult MainMenu()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.FirstOrDefault(x => x.UserId == userId);

            if (user != null)
            {
                return View(user);
            }

            TempData["Message"] = "No data found.";
            TempData["IsSuccess"] = false;

            return RedirectToAction("Login");
        }

        public IActionResult Withdraw()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.FirstOrDefault(x => x.UserId == userId);

            if (user != null)
            {
                return View(user);
            }

            TempData["Message"] = "No data found.";
            TempData["IsSuccess"] = false;

            return RedirectToAction("MainMenu");
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(ATMUserModel reqModel)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

            if (user is null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Json(user);
            }

            if (user.Balance < reqModel.Balance)
            {
                TempData["Message"] = "Withdrawal failed. Invalid data or insufficient balance.";
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
        public IActionResult Deposit()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.FirstOrDefault(x => x.UserId == userId);

            if (user != null)
            {
                return View(user);
            }

            TempData["Message"] = "No data found.";
            TempData["IsSuccess"] = false;

            return RedirectToAction("MainMenu");
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(ATMUserModel reqModel)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

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
        public async Task<IActionResult> CheckBalance()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

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
