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
    public class CentrosEducativoeController : Controller
    {
        private readonly ProtectoScrumMepContext _context;

        public CentrosEducativoeController(ProtectoScrumMepContext context)
        {
            _context = context;
        }

        // GET: CentrosEducativoe
        public async Task<IActionResult> Index()
        {
            var protectoScrumMepContext = _context.CentrosEducativos.Include(c => c.Cantone).Include(c => c.Distrito).Include(c => c.IdProvinciaNavigation).Include(c => c.IdTipoNavigation);
            return View(await protectoScrumMepContext.ToListAsync());
        }

        // GET: CentrosEducativoe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centrosEducativo = await _context.CentrosEducativos
                .Include(c => c.Cantone)
                .Include(c => c.Distrito)
                .Include(c => c.IdProvinciaNavigation)
                .Include(c => c.IdTipoNavigation)
                .FirstOrDefaultAsync(m => m.IdCentro == id);
            if (centrosEducativo == null)
            {
                return NotFound();
            }

            return View(centrosEducativo);
        }

        // GET: CentrosEducativoe/Create
        public IActionResult Create()
        {
            ViewData["IdCanton"] = new SelectList(_context.Cantones, "IdCanton", "IdCanton");
            ViewData["IdDistrito"] = new SelectList(_context.Distritos, "IdDistrito", "IdDistrito");
            ViewData["IdProvincia"] = new SelectList(_context.Provincias, "IdProvincia", "IdProvincia");
            ViewData["IdTipo"] = new SelectList(_context.TipoCentros, "IdTipo", "IdTipo");
            return View();
        }

        // POST: CentrosEducativoe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCentro,NombreCentro,IdTipo,IdProvincia,IdCanton,IdDistrito,Telefono")] CentrosEducativo centrosEducativo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(centrosEducativo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCanton"] = new SelectList(_context.Cantones, "IdCanton", "IdCanton", centrosEducativo.IdCanton);
            ViewData["IdDistrito"] = new SelectList(_context.Distritos, "IdDistrito", "IdDistrito", centrosEducativo.IdDistrito);
            ViewData["IdProvincia"] = new SelectList(_context.Provincias, "IdProvincia", "IdProvincia", centrosEducativo.IdProvincia);
            ViewData["IdTipo"] = new SelectList(_context.TipoCentros, "IdTipo", "IdTipo", centrosEducativo.IdTipo);
            return View(centrosEducativo);
        }

        // GET: CentrosEducativoe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centrosEducativo = await _context.CentrosEducativos.FindAsync(id);
            if (centrosEducativo == null)
            {
                return NotFound();
            }
            ViewData["IdCanton"] = new SelectList(_context.Cantones, "IdCanton", "IdCanton", centrosEducativo.IdCanton);
            ViewData["IdDistrito"] = new SelectList(_context.Distritos, "IdDistrito", "IdDistrito", centrosEducativo.IdDistrito);
            ViewData["IdProvincia"] = new SelectList(_context.Provincias, "IdProvincia", "IdProvincia", centrosEducativo.IdProvincia);
            ViewData["IdTipo"] = new SelectList(_context.TipoCentros, "IdTipo", "IdTipo", centrosEducativo.IdTipo);
            return View(centrosEducativo);
        }

        // POST: CentrosEducativoe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCentro,NombreCentro,IdTipo,IdProvincia,IdCanton,IdDistrito,Telefono")] CentrosEducativo centrosEducativo)
        {
            if (id != centrosEducativo.IdCentro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(centrosEducativo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CentrosEducativoExists(centrosEducativo.IdCentro))
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
            ViewData["IdCanton"] = new SelectList(_context.Cantones, "IdCanton", "IdCanton", centrosEducativo.IdCanton);
            ViewData["IdDistrito"] = new SelectList(_context.Distritos, "IdDistrito", "IdDistrito", centrosEducativo.IdDistrito);
            ViewData["IdProvincia"] = new SelectList(_context.Provincias, "IdProvincia", "IdProvincia", centrosEducativo.IdProvincia);
            ViewData["IdTipo"] = new SelectList(_context.TipoCentros, "IdTipo", "IdTipo", centrosEducativo.IdTipo);
            return View(centrosEducativo);
        }

        // GET: CentrosEducativoe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centrosEducativo = await _context.CentrosEducativos
                .Include(c => c.Cantone)
                .Include(c => c.Distrito)
                .Include(c => c.IdProvinciaNavigation)
                .Include(c => c.IdTipoNavigation)
                .FirstOrDefaultAsync(m => m.IdCentro == id);
            if (centrosEducativo == null)
            {
                return NotFound();
            }

            return View(centrosEducativo);
        }

        // POST: CentrosEducativoe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var centrosEducativo = await _context.CentrosEducativos.FindAsync(id);
            if (centrosEducativo != null)
            {
                _context.CentrosEducativos.Remove(centrosEducativo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CentrosEducativoExists(int id)
        {
            return _context.CentrosEducativos.Any(e => e.IdCentro == id);
        }
    }
}
