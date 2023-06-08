using Microsoft.AspNetCore.Mvc;

namespace Lab7MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return RedirectToAction("Index");
        }
    }
}
