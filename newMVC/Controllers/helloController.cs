using Microsoft.AspNetCore.Mvc;

namespace helloMVCControllers
{
    public class helloController : Controller
  {
    [HttpGet]
    public ActionResult Index()
    {
    return View();
    }

        [HttpPost]
        public ActionResult Index(string fullName)
        {
            ViewBag.Message = "Xin chào " + fullName;
            return View();
        }

  }
}
