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
    public class TipoCentroesController : Controller
    {
        private readonly ProtectoScrumMepContext _context;

        public TipoCentroesController(ProtectoScrumMepContext context)
        {
            _context = context;
        }

        // GET: TipoCentroes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoCentros.ToListAsync());
        }

        // GET: TipoCentroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCentro = await _context.TipoCentros
                .FirstOrDefaultAsync(m => m.IdTipo == id);
            if (tipoCentro == null)
            {
                return NotFound();
            }

            return View(tipoCentro);
        }

        // GET: TipoCentroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoCentroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipo,NombreTipo")] TipoCentro tipoCentro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoCentro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoCentro);
        }

        // GET: TipoCentroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCentro = await _context.TipoCentros.FindAsync(id);
            if (tipoCentro == null)
            {
                return NotFound();
            }
            return View(tipoCentro);
        }

        // POST: TipoCentroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipo,NombreTipo")] TipoCentro tipoCentro)
        {
            if (id != tipoCentro.IdTipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoCentro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoCentroExists(tipoCentro.IdTipo))
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
            return View(tipoCentro);
        }

        // GET: TipoCentroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCentro = await _context.TipoCentros
                .FirstOrDefaultAsync(m => m.IdTipo == id);
            if (tipoCentro == null)
            {
                return NotFound();
            }

            return View(tipoCentro);
        }

        // POST: TipoCentroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoCentro = await _context.TipoCentros.FindAsync(id);
            if (tipoCentro != null)
            {
                _context.TipoCentros.Remove(tipoCentro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoCentroExists(int id)
        {
            return _context.TipoCentros.Any(e => e.IdTipo == id);
        }
    }
}
