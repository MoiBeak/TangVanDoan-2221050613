using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using newMVC.Data;
using newMVC.Models.Entities;

namespace newMVC.Controllers
{
    public class Product1Controller : Controller
    {
        private readonly ApplicationDBContext _context;

        public Product1Controller(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Product1
        public async Task<IActionResult> Index(string searchString)
        {
            var applicationDBContext = _context.Product1
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                applicationDBContext = applicationDBContext.Where(p =>
                    p.Name.Contains(searchString));
            }

            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Product1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product1 = await _context.Product1
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product1 == null)
            {
                return NotFound();
            }

            return View(product1);
        }

        // GET: Product1/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] =
                new SelectList(_context.Categories, "Id", "Name");

            ViewData["SupplierId"] =
                new SelectList(_context.Suppliers, "Id", "Name");

            return View();
        }

        // POST: Product1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,Unit,Quantity,Price,CategoryId,SupplierId")]
            Product1 product1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product1);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] =
                new SelectList(_context.Categories,
                               "Id",
                               "Name",
                               product1.CategoryId);

            ViewData["SupplierId"] =
                new SelectList(_context.Suppliers,
                               "Id",
                               "Name",
                               product1.SupplierId);

            return View(product1);
        }

        // GET: Product1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product1 = await _context.Product1.FindAsync(id);

            if (product1 == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] =
                new SelectList(_context.Categories,
                               "Id",
                               "Name",
                               product1.CategoryId);

            ViewData["SupplierId"] =
                new SelectList(_context.Suppliers,
                               "Id",
                               "Name",
                               product1.SupplierId);

            return View(product1);
        }

        // POST: Product1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Name,Unit,Quantity,Price,CategoryId,SupplierId")]
            Product1 product1)
        {
            if (id != product1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Product1Exists(product1.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] =
                new SelectList(_context.Categories,
                               "Id",
                               "Name",
                               product1.CategoryId);

            ViewData["SupplierId"] =
                new SelectList(_context.Suppliers,
                               "Id",
                               "Name",
                               product1.SupplierId);

            return View(product1);
        }

        // GET: Product1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product1 = await _context.Product1
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product1 == null)
            {
                return NotFound();
            }

            return View(product1);
        }

        // POST: Product1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product1 = await _context.Product1.FindAsync(id);

            if (product1 != null)
            {
                _context.Product1.Remove(product1);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool Product1Exists(int id)
        {
            return _context.Product1.Any(e => e.Id == id);
        }
    }
}