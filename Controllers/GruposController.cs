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
    public class GruposController : Controller
    {
        private readonly ProtectoScrumMepContext _context;

        public GruposController(ProtectoScrumMepContext context)
        {
            _context = context;
        }

        // GET: Grupos
        public async Task<IActionResult> Index()
        {
            var protectoScrumMepContext = _context.Grupos.Include(g => g.IdCentroNavigation).Include(g => g.IdNivelNavigation);
            return View(await protectoScrumMepContext.ToListAsync());
        }

        // Busqueda grupos
        public async Task<IActionResult> Search(string busqueda)
        {
            var grupos = _context.Grupos.Where(a => a.NombreGrupo.Contains(busqueda)).ToList();
            return View("Index", grupos);
        }


        // GET: Grupos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupos
                .Include(g => g.IdCentroNavigation)
                .Include(g => g.IdNivelNavigation)
                .FirstOrDefaultAsync(m => m.IdGrupo == id);
            if (grupo == null)
            {
                return NotFound();
            }

            return View(grupo);
        }

        // GET: Grupos/Create
        public IActionResult Create()
        {
            ViewData["IdCentro"] = new SelectList(_context.CentrosEducativos, "IdCentro", "IdCentro");
            ViewData["IdNivel"] = new SelectList(_context.Niveles, "IdNivel", "IdNivel");
            return View();
        }

        // POST: Grupos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGrupo,IdNivel,NombreGrupo,IdCentro")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grupo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCentro"] = new SelectList(_context.CentrosEducativos, "IdCentro", "IdCentro", grupo.IdCentro);
            ViewData["IdNivel"] = new SelectList(_context.Niveles, "IdNivel", "IdNivel", grupo.IdNivel);
            return View(grupo);
        }

        // GET: Grupos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupos.FindAsync(id);
            if (grupo == null)
            {
                return NotFound();
            }
            ViewData["IdCentro"] = new SelectList(_context.CentrosEducativos, "IdCentro", "IdCentro", grupo.IdCentro);
            ViewData["IdNivel"] = new SelectList(_context.Niveles, "IdNivel", "IdNivel", grupo.IdNivel);
            return View(grupo);
        }

        // POST: Grupos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGrupo,IdNivel,NombreGrupo,IdCentro")] Grupo grupo)
        {
            if (id != grupo.IdGrupo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupoExists(grupo.IdGrupo))
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
            ViewData["IdCentro"] = new SelectList(_context.CentrosEducativos, "IdCentro", "IdCentro", grupo.IdCentro);
            ViewData["IdNivel"] = new SelectList(_context.Niveles, "IdNivel", "IdNivel", grupo.IdNivel);
            return View(grupo);
        }

        // GET: Grupos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupos
                .Include(g => g.IdCentroNavigation)
                .Include(g => g.IdNivelNavigation)
                .FirstOrDefaultAsync(m => m.IdGrupo == id);
            if (grupo == null)
            {
                return NotFound();
            }

            return View(grupo);
        }

        // POST: Grupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grupo = await _context.Grupos.FindAsync(id);
            if (grupo != null)
            {
                _context.Grupos.Remove(grupo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrupoExists(int id)
        {
            return _context.Grupos.Any(e => e.IdGrupo == id);
        }
    }
}
