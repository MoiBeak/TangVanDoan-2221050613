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
    public class ExportInvoiceDetailsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ExportInvoiceDetailsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: ExportInvoiceDetails
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.ExportInvoiceDetails
                .Include(e => e.ExportInvoice)
                .Include(e => e.Product);

            return View(await applicationDBContext.ToListAsync());
        }

        // GET: ExportInvoiceDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportInvoiceDetail = await _context.ExportInvoiceDetails
                .Include(e => e.ExportInvoice)
                .Include(e => e.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (exportInvoiceDetail == null)
            {
                return NotFound();
            }

            return View(exportInvoiceDetail);
        }

        // GET: ExportInvoiceDetails/Create
        public IActionResult Create()
        {
            ViewData["ExportInvoiceId"] =
                new SelectList(_context.ExportInvoices, "Id", "Id");

            ViewData["ProductId"] =
                new SelectList(_context.Product1, "Id", "Name");

            return View();
        }

        // POST: ExportInvoiceDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,ExportInvoiceId,ProductId,Quantity,UnitPrice,TotalPrice")]
            ExportInvoiceDetail exportInvoiceDetail)
        {
            if (ModelState.IsValid)
            {
                // Tính thành tiền
                exportInvoiceDetail.TotalPrice =
                    exportInvoiceDetail.Quantity *
                    exportInvoiceDetail.UnitPrice;

                // Lấy sản phẩm
                var product = await _context.Product1
                    .FindAsync(exportInvoiceDetail.ProductId);

                if (product != null)
                {
                    // Kiểm tra tồn kho
                    if (product.Quantity < exportInvoiceDetail.Quantity)
                    {
                        ModelState.AddModelError("",
                            "Số lượng tồn kho không đủ");

                        ViewData["ExportInvoiceId"] =
                            new SelectList(_context.ExportInvoices,
                                           "Id",
                                           "Id",
                                           exportInvoiceDetail.ExportInvoiceId);

                        ViewData["ProductId"] =
                            new SelectList(_context.Product1,
                                           "Id",
                                           "Name",
                                           exportInvoiceDetail.ProductId);

                        return View(exportInvoiceDetail);
                    }

                    // Trừ tồn kho
                    product.Quantity -= exportInvoiceDetail.Quantity;
                }

                // Thêm chi tiết phiếu xuất
                _context.Add(exportInvoiceDetail);

                await _context.SaveChangesAsync();

                // Tính tổng tiền phiếu xuất
                var invoice = await _context.ExportInvoices
                    .Include(i => i.ExportInvoiceDetails)
                    .FirstOrDefaultAsync(i =>
                        i.Id == exportInvoiceDetail.ExportInvoiceId);

                if (invoice != null)
                {
                    invoice.TotalAmount =
                        invoice.ExportInvoiceDetails.Sum(d => d.TotalPrice);

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ExportInvoiceId"] =
                new SelectList(_context.ExportInvoices,
                               "Id",
                               "Id",
                               exportInvoiceDetail.ExportInvoiceId);

            ViewData["ProductId"] =
                new SelectList(_context.Product1,
                               "Id",
                               "Name",
                               exportInvoiceDetail.ProductId);

            return View(exportInvoiceDetail);
        }

        // GET: ExportInvoiceDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportInvoiceDetail =
                await _context.ExportInvoiceDetails.FindAsync(id);

            if (exportInvoiceDetail == null)
            {
                return NotFound();
            }

            ViewData["ExportInvoiceId"] =
                new SelectList(_context.ExportInvoices,
                               "Id",
                               "Id",
                               exportInvoiceDetail.ExportInvoiceId);

            ViewData["ProductId"] =
                new SelectList(_context.Product1,
                               "Id",
                               "Name",
                               exportInvoiceDetail.ProductId);

            return View(exportInvoiceDetail);
        }

        // POST: ExportInvoiceDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,ExportInvoiceId,ProductId,Quantity,UnitPrice,TotalPrice")]
            ExportInvoiceDetail exportInvoiceDetail)
        {
            if (id != exportInvoiceDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Tính lại thành tiền
                    exportInvoiceDetail.TotalPrice =
                        exportInvoiceDetail.Quantity *
                        exportInvoiceDetail.UnitPrice;

                    _context.Update(exportInvoiceDetail);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExportInvoiceDetailExists(exportInvoiceDetail.Id))
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

            ViewData["ExportInvoiceId"] =
                new SelectList(_context.ExportInvoices,
                               "Id",
                               "Id",
                               exportInvoiceDetail.ExportInvoiceId);

            ViewData["ProductId"] =
                new SelectList(_context.Product1,
                               "Id",
                               "Name",
                               exportInvoiceDetail.ProductId);

            return View(exportInvoiceDetail);
        }

        // GET: ExportInvoiceDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportInvoiceDetail = await _context.ExportInvoiceDetails
                .Include(e => e.ExportInvoice)
                .Include(e => e.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (exportInvoiceDetail == null)
            {
                return NotFound();
            }

            return View(exportInvoiceDetail);
        }

        // POST: ExportInvoiceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exportInvoiceDetail =
                await _context.ExportInvoiceDetails.FindAsync(id);

            if (exportInvoiceDetail != null)
            {
                _context.ExportInvoiceDetails.Remove(exportInvoiceDetail);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ExportInvoiceDetailExists(int id)
        {
            return _context.ExportInvoiceDetails.Any(e => e.Id == id);
        }
    }
}