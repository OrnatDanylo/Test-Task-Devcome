using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Test.Models;

namespace TestTask.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return _context.Users != null ?
                        View(await _context.Users.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var userEntity = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userEntity == null)
            {
                return NotFound();
            }

            return View(userEntity);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Login,Password,FirstName,LastName,DateOfBirth,Gender")] UserEntity userEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(userEntity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex) 
                {
                    TempData["ErrorMessage"] = "Помилка! Уже існує таки користувач.";
                    Console.WriteLine("Не вдалося додати користувача до бази даних. " + ex);
                    return View(userEntity);
                }
            }

            return View(userEntity);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity == null)
            {
                return NotFound();
            }
            return View(userEntity);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Login,Password,FirstName,LastName,DateOfBirth,Gender")] UserEntity userEntity)
        {
            userEntity.UserId = id;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserEntityExists(userEntity.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException ex)
                {
                    TempData["ErrorMessage"] = "Помилка! Уже існує таки користувач.";
                    Console.WriteLine("Не вдалося додати користувача до бази даних. " + ex);
                    return View(userEntity);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userEntity);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var userEntity = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userEntity == null)
            {
                return NotFound();
            }

            return View(userEntity);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'AppDbContext.Users'  is null.");
            }
            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity != null && UserOrderExistCheck(id))
            {
                _context.Users.Remove(userEntity);
            }
   

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserEntityExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }

        private bool UserOrderExistCheck(int id) 
        {
            var isExists = _context.Orders
                                        .Where(o => o.UserId == id)
                                        .Take(1)
                                        .ToList();
            if(isExists.Any()) { 
                TempData["ErrorMessage"] = "Помилка! У юзера існують замовлення."; 
            }
           

            return !isExists.Any();
        }
    }
}
