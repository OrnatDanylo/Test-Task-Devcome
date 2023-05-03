using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Test.Models;

namespace TestTask.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Orders.Include(o => o.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orderEntity = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (orderEntity == null)
            {
                return NotFound();
            }

            return View(orderEntity);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,UserId,OrderDate,OrderCost,ItemsDecription,ShipingAddress")] OrderEntity orderEntity)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Any())
            .Select(x => new { x.Key, x.Value.Errors })
            .ToArray();

            foreach (var error in errors)
            {
                var fieldName = error.Key;
                var validationErrors = error.Errors.Select(e => e.ErrorMessage);
                var errorMessage = string.Join(" ", validationErrors);
                // Вивести повідомлення про помилку
                Console.WriteLine($"Поле {fieldName}: {errorMessage}");
            }
            if (ModelState.IsValid)
            {
                _context.Add(orderEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", orderEntity.UserId);
            return View(orderEntity);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orderEntity = await _context.Orders.FindAsync(id);
            if (orderEntity == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", orderEntity.UserId);
            return View(orderEntity);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,UserId,OrderDate,OrderCost,ItemsDecription,ShipingAddress")] OrderEntity orderEntity)
        {
            if (id != orderEntity.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderEntityExists(orderEntity.OrderID))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", orderEntity.UserId);
            return View(orderEntity);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orderEntity = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (orderEntity == null)
            {
                return NotFound();
            }

            return View(orderEntity);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'AppDbContext.Orders'  is null.");
            }
            var orderEntity = await _context.Orders.FindAsync(id);
            if (orderEntity != null)
            {
                _context.Orders.Remove(orderEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderEntityExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrderID == id)).GetValueOrDefault();
        }
    }
}
