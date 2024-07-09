using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proyectoscrum.Models;


namespace proyectoscrum.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ProtectoScrumMepContext _context;

        public UsuariosController(ProtectoScrumMepContext context)
        {
            _context = context;
        }

        
        // Método para mostrar el formulario de login
        public IActionResult Login()
        {

            ClaimsPrincipal claimsUser = HttpContext.User;

            if (claimsUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Correo, string Contrasenna)
        {
            if (string.IsNullOrEmpty(Correo) || string.IsNullOrEmpty(Contrasenna))
            {
                ViewBag.Error = "Debe proporcionar un correo y una contraseña.";
                return View();
            }

           var usuario = await _context.Usuarios.FirstOrDefaultAsync(p => p.Correo == Correo && p.Contrasenna == Contrasenna);

            if (usuario == null)
            {
                ViewBag.Error = "Correo o contraseña inválidos.";
                return View();
            }

            var usuarioPerfil = await _context.Usuarios.FirstOrDefaultAsync(p => p.IdUsuario == usuario.IdUsuario);

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Name, usuario.Identificacion.ToString()),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuarioPerfil.IdRol.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = true

            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Usuarios");
        }


       

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var protectoScrumMepContext = _context.Usuarios.Include(u => u.IdRolNavigation);
            return View(await protectoScrumMepContext.ToListAsync());
        }

        // Search Usuarios
        public async Task<IActionResult> Search(string busqueda)
        {
            var usuario = _context.Usuarios.Where(a => a.Identificacion.ToString().Contains(busqueda) || a.Nombre.Contains(busqueda) || a.Apellido1.Contains(busqueda)).ToList();
            return View("Index", usuario);
        }


        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "IdRol");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,IdRol,Identificacion,Nombre,Apellido1,Apellido2,Contrasenna,Correo,Telefono,FechaNacimiento")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "IdRol", usuario.IdRol);
 
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "IdRol", usuario.IdRol);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,IdRol,Identificacion,Nombre,Apellido1,Apellido2,Contrasenna,Correo,Telefono,FechaNacimiento")] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUsuario))
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
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "IdRol", usuario.IdRol);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }

        // ROLES

        public ActionResult BuscarRol(string valor)
        {
            // Lógica para buscar roles

            var roles = new List<Role>();
            //var claim = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(valor))
            {
                roles = _context.Roles.ToList();
            }
            else
            {
                roles = _context.Roles.Where(a => a.IdRol.ToString().Contains(valor)).ToList();
            }

            // Devuelve una vista parcial con los resultados
            return PartialView("_ResultadosBusquedaRol", roles);
        }


    }
}
