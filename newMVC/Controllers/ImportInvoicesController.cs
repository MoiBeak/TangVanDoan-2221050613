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
    public class ImportInvoicesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ImportInvoicesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: ImportInvoices
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.ImportInvoices.Include(i => i.Supplier);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: ImportInvoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importInvoices = await _context.ImportInvoices
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (importInvoices == null)
            {
                return NotFound();
            }

            return View(importInvoices);
        }

        // GET: ImportInvoices/Create
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id");
            return View();
        }

        // POST: ImportInvoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImportDate,SupplierId,TotalAmount")] ImportInvoices importInvoices)
        {
            if (ModelState.IsValid)
            {
                _context.Add(importInvoices);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", importInvoices.SupplierId);
            return View(importInvoices);
        }

        // GET: ImportInvoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importInvoices = await _context.ImportInvoices.FindAsync(id);
            if (importInvoices == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", importInvoices.SupplierId);
            return View(importInvoices);
        }

        // POST: ImportInvoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImportDate,SupplierId,TotalAmount")] ImportInvoices importInvoices)
        {
            if (id != importInvoices.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(importInvoices);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportInvoicesExists(importInvoices.Id))
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
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", importInvoices.SupplierId);
            return View(importInvoices);
        }

        // GET: ImportInvoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importInvoices = await _context.ImportInvoices
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (importInvoices == null)
            {
                return NotFound();
            }

            return View(importInvoices);
        }

        // POST: ImportInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var importInvoices = await _context.ImportInvoices.FindAsync(id);
            if (importInvoices != null)
            {
                _context.ImportInvoices.Remove(importInvoices);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImportInvoicesExists(int id)
        {
            return _context.ImportInvoices.Any(e => e.Id == id);
        }
    }
}
