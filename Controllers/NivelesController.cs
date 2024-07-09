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
    public class NivelesController : Controller
    {
        private readonly ProtectoScrumMepContext _context;

        public NivelesController(ProtectoScrumMepContext context)
        {
            _context = context;
        }

        // GET: Niveles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Niveles.ToListAsync());
        }

        // GET: Niveles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nivele = await _context.Niveles
                .FirstOrDefaultAsync(m => m.IdNivel == id);
            if (nivele == null)
            {
                return NotFound();
            }

            return View(nivele);
        }

        // GET: Niveles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Niveles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNivel,NombreNivel")] Nivele nivele)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nivele);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nivele);
        }

        // GET: Niveles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nivele = await _context.Niveles.FindAsync(id);
            if (nivele == null)
            {
                return NotFound();
            }
            return View(nivele);
        }

        // POST: Niveles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNivel,NombreNivel")] Nivele nivele)
        {
            if (id != nivele.IdNivel)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nivele);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NiveleExists(nivele.IdNivel))
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
            return View(nivele);
        }

        // GET: Niveles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nivele = await _context.Niveles
                .FirstOrDefaultAsync(m => m.IdNivel == id);
            if (nivele == null)
            {
                return NotFound();
            }

            return View(nivele);
        }

        // POST: Niveles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nivele = await _context.Niveles.FindAsync(id);
            if (nivele != null)
            {
                _context.Niveles.Remove(nivele);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NiveleExists(int id)
        {
            return _context.Niveles.Any(e => e.IdNivel == id);
        }
    }
}
