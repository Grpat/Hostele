#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hostele.Data;
using Hostele.Models;

namespace Hostele.Controllers
{
    [Area("Staff")]
    public class RodzajController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RodzajController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rodzaj
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rodzaje.ToListAsync());
        }

        // GET: Rodzaj/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rodzaj = await _context.Rodzaje
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rodzaj == null)
            {
                return NotFound();
            }

            return View(rodzaj);
        }

        // GET: Rodzaj/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rodzaj/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NazwaRodzaju")] Rodzaj rodzaj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rodzaj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rodzaj);
        }

        // GET: Rodzaj/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rodzaj = await _context.Rodzaje.FindAsync(id);
            if (rodzaj == null)
            {
                return NotFound();
            }
            return View(rodzaj);
        }

        // POST: Rodzaj/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NazwaRodzaju")] Rodzaj rodzaj)
        {
            if (id != rodzaj.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rodzaj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RodzajExists(rodzaj.Id))
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
            return View(rodzaj);
        }

        // GET: Rodzaj/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rodzaj = await _context.Rodzaje
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rodzaj == null)
            {
                return NotFound();
            }

            return View(rodzaj);
        }

        // POST: Rodzaj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rodzaj = await _context.Rodzaje.FindAsync(id);
            _context.Rodzaje.Remove(rodzaj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RodzajExists(int id)
        {
            return _context.Rodzaje.Any(e => e.Id == id);
        }
    }
}
