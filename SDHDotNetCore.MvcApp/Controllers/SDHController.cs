using Microsoft.AspNetCore.Mvc;

namespace SDHDotNetCore.MvcApp.Controllers
{
    public class SDHController : Controller
    {
        [ActionName("Index")]
        public IActionResult SDHIndex()
        {
            string t = "Gone With  The Wind";
            string a = "Margaret Mitchell";

            ViewData["Title"] = t;
            ViewData["Author"] = a;
            ViewData["Content"] = "Testing ViewData"; 

            ViewBag.Title = t;
            ViewBag.Author = a;
            ViewBag.Content = "Testing ViewBag";

            TempData["Title"] = t;
            TempData["Author"] = a;
            TempData["Content"] = "Testing TempData";

            return View("SDHIndex");
        }
    }
}
