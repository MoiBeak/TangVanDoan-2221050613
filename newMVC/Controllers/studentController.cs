//  using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using newMVC.Models;

  public class StudentController : Controller
  {
  [HttpGet]
  public ActionResult Index()
  {
  return View();
  }

        [HttpPost]
        public ActionResult Index(Student student)
        {
            ViewBag.Message = $"Xin chào {student.FullName} - Mã SV: {student.StudentCode}";
            return View(student);
        }

  }