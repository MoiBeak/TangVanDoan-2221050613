using Microsoft.AspNetCore.Mvc;
using newMVC.Data;
using newMVC.Models;
using System.Linq;

namespace newMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDBContext _context;

        public StudentController(ApplicationDBContext context)
        {
            _context = context;
        }

        // READ
        public IActionResult Index()
        {
            var students = _context.Students.ToList();
            return View(students);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // EDIT (GET)
        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();

            return View(student);
        }

        // EDIT (POST)
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // DELETE (GET)
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();

            return View(student);
        }

        // DELETE (POST)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);
            _context.Students.Remove(student);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}