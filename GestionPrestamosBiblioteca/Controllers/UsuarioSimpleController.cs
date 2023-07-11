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

    public class UsuarioSimpleController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public UsuarioSimpleController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: UsuarioSimpleController
        [HttpGet]
        public async Task<IActionResult> GetListaUsuarioSimple()
        {
            try
            {
                var listUsuarioSimples = await _context.UsuarioSimple.ToListAsync();
                return Ok(listUsuarioSimples);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: UsuarioSimpleController/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioSimple(int id)
        {
            try
            {
                var usuarioSimple = await _context.UsuarioSimple.FindAsync(id);
                if (usuarioSimple == null)
                {
                    return NotFound();
                }
                return Ok(usuarioSimple);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: UsuarioSimpleController/Create
        [HttpPost]
        public async Task<IActionResult> RegistrarUsuarioSimple([FromBody] UsuarioSimple usuarioSimple)
        {
            try
            {
                _context.Add(usuarioSimple);
                await _context.SaveChangesAsync();
                return Ok(usuarioSimple);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: UsuarioSimpleController/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioSimple(int id)
        {
            try
            {
                var usuarioSimple = await _context.UsuarioSimple.FindAsync(id);
                if (usuarioSimple == null)
                {
                    return NotFound();
                }
                _context.UsuarioSimple.Remove(usuarioSimple);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Usuario eliminado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: UsuarioSimpleController/Edit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarUsuarioSimple(int id, [FromBody] UsuarioSimple usuarioSimple)
        {
            try
            {
                if (id != usuarioSimple.Id)
                {
                    return BadRequest();
                }

                var usuarioExistente = await _context.UsuarioSimple
                    .Include(u => u.Prestamos) // Incluir la relación con los préstamos
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (usuarioExistente == null)
                {
                    return NotFound();
                }

                // Actualizar las propiedades del usuario existente
                usuarioExistente.Nombre = usuarioSimple.Nombre;
                usuarioExistente.Direccion = usuarioSimple.Direccion;
                usuarioExistente.Telefono = usuarioSimple.Telefono;
                usuarioExistente.Documento = usuarioSimple.Documento;
                usuarioExistente.Mail = usuarioSimple.Mail;

                // Actualizar las propiedades de los préstamos relacionados
                foreach (var prestamo in usuarioSimple.Prestamos)
                {
                    var prestamoExistente = usuarioExistente.Prestamos.FirstOrDefault(p => p.Id == prestamo.Id);
                    if (prestamoExistente != null)
                    {
                        prestamoExistente.FechaInicio = prestamo.FechaInicio;
                        prestamoExistente.FechaFin = prestamo.FechaFin;
                        prestamoExistente.FechaVencimiento = prestamo.FechaVencimiento;
                    }
                }

                _context.Update(usuarioExistente);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Usuario actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}