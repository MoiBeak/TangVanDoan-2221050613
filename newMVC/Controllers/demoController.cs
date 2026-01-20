using Microsoft.AspNetCore.Mvc;

namespace demoMVCControllers
{
    public class demoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
