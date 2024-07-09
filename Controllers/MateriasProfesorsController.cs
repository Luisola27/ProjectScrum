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
    public class MateriasProfesorsController : Controller
    {
        private readonly ProtectoScrumMepContext _context;

        public MateriasProfesorsController(ProtectoScrumMepContext context)
        {
            _context = context;
        }

        // GET: MateriasProfesors
        public async Task<IActionResult> Index()
        {
            var protectoScrumMepContext = _context.MateriasProfesors.Include(m => m.IdMateriaNavigation).Include(m => m.IdProfesorNavigation);
            return View(await protectoScrumMepContext.ToListAsync());
        }

        // GET: MateriasProfesors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiasProfesor = await _context.MateriasProfesors
                .Include(m => m.IdMateriaNavigation)
                .Include(m => m.IdProfesorNavigation)
                .FirstOrDefaultAsync(m => m.IdMateriaProfesor == id);
            if (materiasProfesor == null)
            {
                return NotFound();
            }

            return View(materiasProfesor);
        }

        // GET: MateriasProfesors/Create
        public IActionResult Create()
        {
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "IdMateria");
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "IdProfesor", "IdProfesor");
            return View();
        }

        // POST: MateriasProfesors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMateriaProfesor,IdProfesor,IdMateria")] MateriasProfesor materiasProfesor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materiasProfesor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "IdMateria", materiasProfesor.IdMateria);
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "IdProfesor", "IdProfesor", materiasProfesor.IdProfesor);
            return View(materiasProfesor);
        }

        // GET: MateriasProfesors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiasProfesor = await _context.MateriasProfesors.FindAsync(id);
            if (materiasProfesor == null)
            {
                return NotFound();
            }
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "IdMateria", materiasProfesor.IdMateria);
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "IdProfesor", "IdProfesor", materiasProfesor.IdProfesor);
            return View(materiasProfesor);
        }

        // POST: MateriasProfesors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMateriaProfesor,IdProfesor,IdMateria")] MateriasProfesor materiasProfesor)
        {
            if (id != materiasProfesor.IdMateriaProfesor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materiasProfesor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MateriasProfesorExists(materiasProfesor.IdMateriaProfesor))
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
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "IdMateria", materiasProfesor.IdMateria);
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "IdProfesor", "IdProfesor", materiasProfesor.IdProfesor);
            return View(materiasProfesor);
        }

        // GET: MateriasProfesors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiasProfesor = await _context.MateriasProfesors
                .Include(m => m.IdMateriaNavigation)
                .Include(m => m.IdProfesorNavigation)
                .FirstOrDefaultAsync(m => m.IdMateriaProfesor == id);
            if (materiasProfesor == null)
            {
                return NotFound();
            }

            return View(materiasProfesor);
        }

        // POST: MateriasProfesors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materiasProfesor = await _context.MateriasProfesors.FindAsync(id);
            if (materiasProfesor != null)
            {
                _context.MateriasProfesors.Remove(materiasProfesor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MateriasProfesorExists(int id)
        {
            return _context.MateriasProfesors.Any(e => e.IdMateriaProfesor == id);
        }
    }
}
