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
    public class NotaEstudiantesController : Controller
    {
        private readonly ProtectoScrumMepContext _context;

        public NotaEstudiantesController(ProtectoScrumMepContext context)
        {
            _context = context;
        }

        // GET: NotaEstudiantes
        public async Task<IActionResult> Index()
        {
            var protectoScrumMepContext = _context.NotaEstudiantes.Include(n => n.IdMateriaNavigation).Include(n => n.IdUsuarioNavigation);
            return View(await protectoScrumMepContext.ToListAsync());
        }

        // GET: NotaEstudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaEstudiante = await _context.NotaEstudiantes
                .Include(n => n.IdMateriaNavigation)
                .Include(n => n.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdNotaEst == id);
            if (notaEstudiante == null)
            {
                return NotFound();
            }

            return View(notaEstudiante);
        }

        // GET: NotaEstudiantes/Create
        public IActionResult Create()
        {
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "IdMateria");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: NotaEstudiantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNotaEst,IdUsuario,IdMateria,Rubica,PuntajeObtenido")] NotaEstudiante notaEstudiante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notaEstudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "IdMateria", notaEstudiante.IdMateria);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", notaEstudiante.IdUsuario);
            return View(notaEstudiante);
        }

        // GET: NotaEstudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaEstudiante = await _context.NotaEstudiantes.FindAsync(id);
            if (notaEstudiante == null)
            {
                return NotFound();
            }
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "IdMateria", notaEstudiante.IdMateria);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", notaEstudiante.IdUsuario);
            return View(notaEstudiante);
        }

        // POST: NotaEstudiantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNotaEst,IdUsuario,IdMateria,Rubica,PuntajeObtenido")] NotaEstudiante notaEstudiante)
        {
            if (id != notaEstudiante.IdNotaEst)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notaEstudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaEstudianteExists(notaEstudiante.IdNotaEst))
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
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "IdMateria", notaEstudiante.IdMateria);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", notaEstudiante.IdUsuario);
            return View(notaEstudiante);
        }

        // GET: NotaEstudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaEstudiante = await _context.NotaEstudiantes
                .Include(n => n.IdMateriaNavigation)
                .Include(n => n.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdNotaEst == id);
            if (notaEstudiante == null)
            {
                return NotFound();
            }

            return View(notaEstudiante);
        }

        //LK
        //Get: Notas Estudiantes
        public async Task<IActionResult> ObtenerNotas(int usuarioId)
        {
            var notas = await _context.GetNotasEstudianteAsync(usuarioId);
            return View(notas);
        }

        // POST: NotaEstudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notaEstudiante = await _context.NotaEstudiantes.FindAsync(id);
            if (notaEstudiante != null)
            {
                _context.NotaEstudiantes.Remove(notaEstudiante);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaEstudianteExists(int id)
        {
            return _context.NotaEstudiantes.Any(e => e.IdNotaEst == id);
        }
    }
}
