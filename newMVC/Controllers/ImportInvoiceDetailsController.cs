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
    public class ImportInvoiceDetailsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ImportInvoiceDetailsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: ImportInvoiceDetails
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.ImportInvoiceDetails
                .Include(i => i.ImportInvoice)
                .Include(i => i.Product);

            return View(await applicationDBContext.ToListAsync());
        }

        // GET: ImportInvoiceDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importInvoiceDetail = await _context.ImportInvoiceDetails
                .Include(i => i.ImportInvoice)
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (importInvoiceDetail == null)
            {
                return NotFound();
            }

            return View(importInvoiceDetail);
        }

        // GET: ImportInvoiceDetails/Create
        public IActionResult Create()
        {
            ViewData["ImportInvoiceId"] =
                new SelectList(_context.ImportInvoices, "Id", "Id");

            ViewData["ProductId"] =
                new SelectList(_context.Product1, "Id", "Name");

            return View();
        }

        // POST: ImportInvoiceDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,ImportInvoiceId,ProductId,Quantity,UnitPrice,TotalPrice")]
            ImportInvoiceDetail importInvoiceDetail)
        {
            if (ModelState.IsValid)
            {
                // Tính thành tiền
                importInvoiceDetail.TotalPrice =
                    importInvoiceDetail.Quantity *
                    importInvoiceDetail.UnitPrice;

                // Tăng tồn kho
                var product = await _context.Product1
                    .FindAsync(importInvoiceDetail.ProductId);

                if (product != null)
                {
                    product.Quantity += importInvoiceDetail.Quantity;
                }

                // Thêm chi tiết phiếu nhập
                _context.Add(importInvoiceDetail);

                await _context.SaveChangesAsync();

                // Tính tổng tiền phiếu nhập
                var invoice = await _context.ImportInvoices
                    .Include(i => i.ImportInvoiceDetails)
                    .FirstOrDefaultAsync(i =>
                        i.Id == importInvoiceDetail.ImportInvoiceId);

                if (invoice != null)
                {
                    invoice.TotalAmount =
                        invoice.ImportInvoiceDetails.Sum(d => d.TotalPrice);

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ImportInvoiceId"] =
                new SelectList(_context.ImportInvoices,
                               "Id",
                               "Id",
                               importInvoiceDetail.ImportInvoiceId);

            ViewData["ProductId"] =
                new SelectList(_context.Product1,
                               "Id",
                               "Name",
                               importInvoiceDetail.ProductId);

            return View(importInvoiceDetail);
        }

        // GET: ImportInvoiceDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importInvoiceDetail =
                await _context.ImportInvoiceDetails.FindAsync(id);

            if (importInvoiceDetail == null)
            {
                return NotFound();
            }

            ViewData["ImportInvoiceId"] =
                new SelectList(_context.ImportInvoices,
                               "Id",
                               "Id",
                               importInvoiceDetail.ImportInvoiceId);

            ViewData["ProductId"] =
                new SelectList(_context.Product1,
                               "Id",
                               "Name",
                               importInvoiceDetail.ProductId);

            return View(importInvoiceDetail);
        }

        // POST: ImportInvoiceDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,ImportInvoiceId,ProductId,Quantity,UnitPrice,TotalPrice")]
            ImportInvoiceDetail importInvoiceDetail)
        {
            if (id != importInvoiceDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Tính lại thành tiền
                    importInvoiceDetail.TotalPrice =
                        importInvoiceDetail.Quantity *
                        importInvoiceDetail.UnitPrice;

                    _context.Update(importInvoiceDetail);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportInvoiceDetailExists(importInvoiceDetail.Id))
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

            ViewData["ImportInvoiceId"] =
                new SelectList(_context.ImportInvoices,
                               "Id",
                               "Id",
                               importInvoiceDetail.ImportInvoiceId);

            ViewData["ProductId"] =
                new SelectList(_context.Product1,
                               "Id",
                               "Name",
                               importInvoiceDetail.ProductId);

            return View(importInvoiceDetail);
        }

        // GET: ImportInvoiceDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importInvoiceDetail = await _context.ImportInvoiceDetails
                .Include(i => i.ImportInvoice)
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (importInvoiceDetail == null)
            {
                return NotFound();
            }

            return View(importInvoiceDetail);
        }

        // POST: ImportInvoiceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var importInvoiceDetail =
                await _context.ImportInvoiceDetails.FindAsync(id);

            if (importInvoiceDetail != null)
            {
                _context.ImportInvoiceDetails.Remove(importInvoiceDetail);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ImportInvoiceDetailExists(int id)
        {
            return _context.ImportInvoiceDetails.Any(e => e.Id == id);
        }
    }
}