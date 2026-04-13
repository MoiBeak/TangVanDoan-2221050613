using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newMVC.Data;
using newMVC.Models.Entities;

namespace newMVC.Controllers
{
    public class FacultyController : Controller
    {
        private readonly ApplicationDBContext _context;

        public FacultyController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: /Faculty
        public async Task<IActionResult> Index()
        {
            var faculties = await _context.Faculties.ToListAsync();
            return View(faculties);
        }

        // GET: /Faculty/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Faculty/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faculty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faculty);
        }

        // GET: /Faculty/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null)
                return NotFound();

            return View(faculty);
        }

        // POST: /Faculty/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Faculty faculty)
        {
            if (id != faculty.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(faculty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faculty);
        }

        // GET: /Faculty/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var faculty = await _context.Faculties
                                        .FirstOrDefaultAsync(m => m.Id == id);
            if (faculty == null)
                return NotFound();

            return View(faculty);
        }

        // POST: /Faculty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty != null)
            {
                _context.Faculties.Remove(faculty);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
