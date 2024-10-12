using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SampleCrudStoreAspNetCore8WithIndentity.Data;
using SampleCrudStoreAspNetCore8WithIndentity.Models;

namespace SampleCrudStoreAspNetCore8WithIndentity.Controllers
{
    public class OrderAppsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderAppsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderApps
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderApp.Include(o => o.Customer).Include(o => o.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderApps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderApp = await _context.OrderApp
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderApp == null)
            {
                return NotFound();
            }

            return View(orderApp);
        }

        // GET: OrderApps/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customer, "Id", "Name");
            ViewData["ProductID"] = new SelectList(_context.Product, "Id", "Name");
            return View();
        }

        // POST: OrderApps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerID,ProductID,Quantity,Price,PriceAll,CreatAt,UpdateAt")] OrderApp orderApp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderApp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "Id", "Name", orderApp.CustomerID);
            ViewData["ProductID"] = new SelectList(_context.Product, "Id", "Name", orderApp.ProductID);
            return View(orderApp);
        }

        // GET: OrderApps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderApp = await _context.OrderApp.FindAsync(id);
            if (orderApp == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "Id", "Name", orderApp.CustomerID);
            ViewData["ProductID"] = new SelectList(_context.Product, "Id", "Name", orderApp.ProductID);
            return View(orderApp);
        }

        // POST: OrderApps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerID,ProductID,Quantity,Price,PriceAll,CreatAt,UpdateAt")] OrderApp orderApp)
        {
            if (id != orderApp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderApp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderAppExists(orderApp.Id))
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
            ViewData["CustomerID"] = new SelectList(_context.Customer, "Id", "Name", orderApp.CustomerID);
            ViewData["ProductID"] = new SelectList(_context.Product, "Id", "Name", orderApp.ProductID);
            return View(orderApp);
        }

        // GET: OrderApps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderApp = await _context.OrderApp
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderApp == null)
            {
                return NotFound();
            }

            return View(orderApp);
        }

        // POST: OrderApps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderApp = await _context.OrderApp.FindAsync(id);
            if (orderApp != null)
            {
                _context.OrderApp.Remove(orderApp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderAppExists(int id)
        {
            return _context.OrderApp.Any(e => e.Id == id);
        }
    }
}
