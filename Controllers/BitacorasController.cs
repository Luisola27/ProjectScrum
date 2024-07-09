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
    public class BitacorasController : Controller
    {
        private readonly ProtectoScrumMepContext _context;

        public BitacorasController(ProtectoScrumMepContext context)
        {
            _context = context;
        }

        // GET: Bitacoras
        public async Task<IActionResult> Index()
        {
            var protectoScrumMepContext = _context.Bitacoras.Include(b => b.IdUsuarioNavigation);
            return View(await protectoScrumMepContext.ToListAsync());
        }

        // GET: Bitacoras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bitacora = await _context.Bitacoras
                .Include(b => b.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdBitacora == id);
            if (bitacora == null)
            {
                return NotFound();
            }

            return View(bitacora);
        }

        // GET: Bitacoras/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: Bitacoras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBitacora,Accion,Afectado,IdUsuario,FechaHora")] Bitacora bitacora)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bitacora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", bitacora.IdUsuario);
            return View(bitacora);
        }

        // GET: Bitacoras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bitacora = await _context.Bitacoras.FindAsync(id);
            if (bitacora == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", bitacora.IdUsuario);
            return View(bitacora);
        }

        // POST: Bitacoras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBitacora,Accion,Afectado,IdUsuario,FechaHora")] Bitacora bitacora)
        {
            if (id != bitacora.IdBitacora)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bitacora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BitacoraExists(bitacora.IdBitacora))
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", bitacora.IdUsuario);
            return View(bitacora);
        }

        // GET: Bitacoras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bitacora = await _context.Bitacoras
                .Include(b => b.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdBitacora == id);
            if (bitacora == null)
            {
                return NotFound();
            }

            return View(bitacora);
        }

        // POST: Bitacoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bitacora = await _context.Bitacoras.FindAsync(id);
            if (bitacora != null)
            {
                _context.Bitacoras.Remove(bitacora);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BitacoraExists(int id)
        {
            return _context.Bitacoras.Any(e => e.IdBitacora == id);
        }
    }
}
