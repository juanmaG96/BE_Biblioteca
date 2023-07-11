using GestionPrestamosBiblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionPrestamosBiblioteca.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class UsuarioAdmController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public UsuarioAdmController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: UsuarioAdmController
        [HttpGet]
        public async Task<IActionResult> GetListaUsuarioAdm()
        {
            try
            {
                var listUsuarioAdm = await _context.UsuarioAdministrador.ToListAsync();
                return Ok(listUsuarioAdm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: UsuarioAdmController/Details/5
        [HttpGet("{nombreUsuario}")]
        public async Task<IActionResult> GetUsuarioAdm(string nombreUsuario)
        {
            try
            {
                var usuarioAdm = await _context.UsuarioAdministrador.FindAsync(nombreUsuario);
                if (usuarioAdm == null)
                {
                    return NotFound();
                }
                return Ok(usuarioAdm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: UsuarioAdmController/Create
        [HttpPost]
        public async Task<IActionResult> RegistrarUsuarioAdm([FromBody] UsuarioAdministrador usuarioAdm)
        {
            try
            {
                _context.Add(usuarioAdm);
                await _context.SaveChangesAsync();
                return Ok(usuarioAdm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: UsuarioAdmController/Delete/5
        [HttpDelete("{nombreUsuario}")]
        public async Task<IActionResult> DeleteUsuarioAdm(string nombreUsuario)
        {
            try
            {
                var usuarioAdm = await _context.UsuarioAdministrador.FindAsync(nombreUsuario);
                if (usuarioAdm == null)
                {
                    return NotFound();
                }
                _context.UsuarioAdministrador.Remove(usuarioAdm);
                await _context.SaveChangesAsync();
                return Ok(new { message = "UsuarioAdm eliminado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: UsuarioAdmController/Edit/5
        [HttpPut("{nombreUsuario}")]
        public async Task<IActionResult> EditarUsuarioAdm(string nombreUsuario, [FromBody] UsuarioAdministrador usuarioAdm)
        {
            try
            {
                if (nombreUsuario != usuarioAdm.NombreUsuario)
                {
                    return BadRequest();
                }
                _context.Update(usuarioAdm);
                await _context.SaveChangesAsync();
                return Ok(new { message = "UsuarioAdm actualizado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("login/admin")]
        public async Task<IActionResult> AdminLogin(UsuarioAdministrador user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var admin = user.NombreUsuario;
            var usuarioAdm = await _context.UsuarioAdministrador.FindAsync(admin);
            // Verificar las credenciales del administrador
            if (user.NombreUsuario == usuarioAdm.NombreUsuario && user.Contrasena == usuarioAdm.Contrasena)
            {
                // Las credenciales son válidas, establecer la sesión del administrador
                HttpContext.Session.SetString("AdminSession", user.NombreUsuario);
                return Ok(new { message = "Inicio de sesión exitoso" });
            }
            else
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }
        }


    }
}
