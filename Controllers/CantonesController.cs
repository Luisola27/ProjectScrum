using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proyectoscrum.Models;

namespace proyectoscrum.Controllers
{
    public class CantonesController : Controller
    {
        private readonly ProtectoScrumMepContext _context;

        public CantonesController(ProtectoScrumMepContext context)
        {
            _context = context;
        }

        // GET: Cantones
        public async Task<IActionResult> Index()
        {
            var protectoScrumMepContext = _context.Cantones.Include(c => c.IdProvinciaNavigation);
            return View(await protectoScrumMepContext.ToListAsync());
        }

        // GET: Cantones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cantone = await _context.Cantones
                .Include(c => c.IdProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.IdCanton == id);
            if (cantone == null)
            {
                return NotFound();
            }

            return View(cantone);
        }

        // GET: Cantones/Create
        public IActionResult Create()
        {
            ViewData["IdProvincia"] = new SelectList(_context.Provincias, "IdProvincia", "IdProvincia");
            return View();
        }

        // POST: Cantones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCanton,IdProvincia,NombreCanton")] Cantone cantone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cantone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProvincia"] = new SelectList(_context.Provincias, "IdProvincia", "IdProvincia", cantone.IdProvincia);
            return View(cantone);
        }

        // GET: Cantones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cantone = await _context.Cantones.FindAsync(id);
            if (cantone == null)
            {
                return NotFound();
            }
            ViewData["IdProvincia"] = new SelectList(_context.Provincias, "IdProvincia", "IdProvincia", cantone.IdProvincia);
            return View(cantone);
        }

        // POST: Cantones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCanton,IdProvincia,NombreCanton")] Cantone cantone)
        {
            if (id != cantone.IdCanton)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cantone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CantoneExists(cantone.IdCanton))
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
            ViewData["IdProvincia"] = new SelectList(_context.Provincias, "IdProvincia", "IdProvincia", cantone.IdProvincia);
            return View(cantone);
        }

        // GET: Cantones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cantone = await _context.Cantones
                .Include(c => c.IdProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.IdCanton == id);
            if (cantone == null)
            {
                return NotFound();
            }

            return View(cantone);
        }

        // POST: Cantones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cantone = await _context.Cantones.FindAsync(id);
            if (cantone != null)
            {
                _context.Cantones.Remove(cantone);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CantoneExists(int id)
        {
            return _context.Cantones.Any(e => e.IdCanton == id);
        }
    }
}
