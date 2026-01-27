using Microsoft.AspNetCore.Mvc;

namespace newMVC.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}