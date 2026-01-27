using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using newMVC.Models;

namespace newMVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
       ViewBag.Message = "Chào mừng đến với ASP.NET MVC";
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
