using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using newMVC.Data;
using newMVC.Models.Entities;

namespace newMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDBContext _context;

        public StudentController(ApplicationDBContext context)
        {
            _context = context;
        }

        // READ: Hiển thị danh sách sinh viên kèm tên khoa
        public IActionResult Index()
        {
            var students = _context.Students
                                   .Include(s => s.Faculty)
                                   .ToList();
            return View(students);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            ViewBag.FacultyId = new SelectList(_context.Faculties, "Id", "FacultyName");
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.FacultyId = new SelectList(_context.Faculties, "Id", "FacultyName", student.FacultyId);
            return View(student);
        }

        // EDIT (GET)
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();

            ViewBag.FacultyId = new SelectList(_context.Faculties, "Id", "FacultyName", student.FacultyId);
            return View(student);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Student student)
        {
            if (id != student.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.FacultyId = new SelectList(_context.Faculties, "Id", "FacultyName", student.FacultyId);
            return View(student);
        }

        // DELETE (GET)
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var student = _context.Students
                                  .Include(s => s.Faculty)
                                  .FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        // DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);

            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
