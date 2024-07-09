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
    public class EstudiantesGruposController : Controller
    {
        private readonly ProtectoScrumMepContext _context;

        public EstudiantesGruposController(ProtectoScrumMepContext context)
        {
            _context = context;
        }

        // GET: EstudiantesGrupos
        public async Task<IActionResult> Index()
        {
            var protectoScrumMepContext = _context.EstudiantesGrupos.Include(e => e.IdGrupoNavigation).Include(e => e.IdUsuarioNavigation);
            return View(await protectoScrumMepContext.ToListAsync());
        }

        // GET: EstudiantesGrupos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiantesGrupo = await _context.EstudiantesGrupos
                .Include(e => e.IdGrupoNavigation)
                .Include(e => e.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdEstudianteGrupo == id);
            if (estudiantesGrupo == null)
            {
                return NotFound();
            }

            return View(estudiantesGrupo);
        }

        // GET: EstudiantesGrupos/Create
        public IActionResult Create()
        {
            ViewData["IdGrupo"] = new SelectList(_context.Grupos, "IdGrupo", "IdGrupo");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: EstudiantesGrupos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,IdGrupo,IdNota,Notafinal,IdEstudianteGrupo")] EstudiantesGrupo estudiantesGrupo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudiantesGrupo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGrupo"] = new SelectList(_context.Grupos, "IdGrupo", "IdGrupo", estudiantesGrupo.IdGrupo);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", estudiantesGrupo.IdUsuario);
            return View(estudiantesGrupo);
        }

        // GET: EstudiantesGrupos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiantesGrupo = await _context.EstudiantesGrupos.FindAsync(id);
            if (estudiantesGrupo == null)
            {
                return NotFound();
            }
            ViewData["IdGrupo"] = new SelectList(_context.Grupos, "IdGrupo", "IdGrupo", estudiantesGrupo.IdGrupo);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", estudiantesGrupo.IdUsuario);
            return View(estudiantesGrupo);
        }

        // POST: EstudiantesGrupos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,IdGrupo,IdNota,Notafinal,IdEstudianteGrupo")] EstudiantesGrupo estudiantesGrupo)
        {
            if (id != estudiantesGrupo.IdEstudianteGrupo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudiantesGrupo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudiantesGrupoExists(estudiantesGrupo.IdEstudianteGrupo))
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
            ViewData["IdGrupo"] = new SelectList(_context.Grupos, "IdGrupo", "IdGrupo", estudiantesGrupo.IdGrupo);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", estudiantesGrupo.IdUsuario);
            return View(estudiantesGrupo);
        }

        // GET: EstudiantesGrupos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiantesGrupo = await _context.EstudiantesGrupos
                .Include(e => e.IdGrupoNavigation)
                .Include(e => e.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdEstudianteGrupo == id);
            if (estudiantesGrupo == null)
            {
                return NotFound();
            }

            return View(estudiantesGrupo);
        }

        // POST: EstudiantesGrupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiantesGrupo = await _context.EstudiantesGrupos.FindAsync(id);
            if (estudiantesGrupo != null)
            {
                _context.EstudiantesGrupos.Remove(estudiantesGrupo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudiantesGrupoExists(int id)
        {
            return _context.EstudiantesGrupos.Any(e => e.IdEstudianteGrupo == id);
        }
    }
}
