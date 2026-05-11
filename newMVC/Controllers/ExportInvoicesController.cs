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
    public class ExportInvoicesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ExportInvoicesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: ExportInvoices
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExportInvoices.ToListAsync());
        }

        // GET: ExportInvoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportInvoices = await _context.ExportInvoices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exportInvoices == null)
            {
                return NotFound();
            }

            return View(exportInvoices);
        }

        // GET: ExportInvoices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExportInvoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExportDate,ReceiverName,TotalAmount")] ExportInvoices exportInvoices)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exportInvoices);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exportInvoices);
        }

        // GET: ExportInvoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportInvoices = await _context.ExportInvoices.FindAsync(id);
            if (exportInvoices == null)
            {
                return NotFound();
            }
            return View(exportInvoices);
        }

        // POST: ExportInvoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExportDate,ReceiverName,TotalAmount")] ExportInvoices exportInvoices)
        {
            if (id != exportInvoices.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exportInvoices);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExportInvoicesExists(exportInvoices.Id))
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
            return View(exportInvoices);
        }

        // GET: ExportInvoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportInvoices = await _context.ExportInvoices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exportInvoices == null)
            {
                return NotFound();
            }

            return View(exportInvoices);
        }

        // POST: ExportInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exportInvoices = await _context.ExportInvoices.FindAsync(id);
            if (exportInvoices != null)
            {
                _context.ExportInvoices.Remove(exportInvoices);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExportInvoicesExists(int id)
        {
            return _context.ExportInvoices.Any(e => e.Id == id);
        }
    }
}
