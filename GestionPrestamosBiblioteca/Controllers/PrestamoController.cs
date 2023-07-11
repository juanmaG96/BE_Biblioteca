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
    //[Route("api/[Controller]")]
    [Route("api/Prestamo")] // Prefijo de ruta para todos los métodos en PrestamoController
    [ApiController]

    public class PrestamoController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public PrestamoController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: PrestamoController
        [HttpGet]
        public async Task<IActionResult> GetListaPrestamos()
        {
            try
            {
                var listPrestamos = await _context.Prestamo.ToListAsync();
                return Ok(listPrestamos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: PrestamoController/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrestamo(int id)
        {
            try
            {
                var prestamo = await _context.Prestamo.FindAsync(id);
                if (prestamo == null)
                {
                    return NotFound();
                }
                return Ok(prestamo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: PrestamoController/Create
        [HttpPost]
        public async Task<IActionResult> RegistrarPrestamo(int ejemplarId, int usuarioId, [FromBody] Prestamo prestamo)
        {
            try
            {
                // Buscar el ejemplar y el usuario en la base de datos.
                var ejemplar = await _context.Ejemplar.FindAsync(ejemplarId);
                var usuario = await _context.UsuarioSimple.FindAsync(usuarioId);

                // Verificar si el ejemplar y el usuario existen.
                if (ejemplar == null || usuario == null)
                {
                    return BadRequest("Ejemplar o usuario no encontrado.");
                }

                // Verificar si el ejemplar está disponible para préstamo.
                if (ejemplar.Prestado)
                {
                    return BadRequest("El ejemplar ya está prestado.");
                }

                // Calcular la fecha de devolución sumando 5 días hábiles a la fecha actual.
                DateTime fechaInicio = DateTime.Now;
                DateTime fechaVencimiento = CalcularFechaVencimiento(fechaInicio);

                // Marcar el ejemplar como prestado.
                ejemplar.Prestado = true;

                // Crear el objeto de préstamo con los datos proporcionados.
                var nuevoPrestamo = new Prestamo
                {
                    FechaInicio = fechaInicio,
                    FechaFin = DateTime.MinValue, // Insertar un campo DateTime vacío
                    FechaVencimiento = fechaVencimiento,
                    EjemplarId = ejemplarId,
                    UsuarioId = usuarioId
                };



                // Agregar el nuevo préstamo a las colecciones correspondientes.
                usuario.Prestamos.Add(nuevoPrestamo);
                ejemplar.Prestamos.Add(nuevoPrestamo);

                // Actualizar las propiedades de navegación
                nuevoPrestamo.UsuarioSimple = usuario;
                nuevoPrestamo.Ejemplar = ejemplar;

                // Guardar el préstamo y los cambios en la base de datos.
                _context.Prestamo.Add(nuevoPrestamo);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Préstamo registrado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private DateTime CalcularFechaVencimiento(DateTime fechaInicio)
        {
            DateTime fechaVencimiento = fechaInicio;

            // Sumar 5 días hábiles a la fecha de inicio.
            int diasHabiles = 0;
            while (diasHabiles < 5)
            {
                fechaVencimiento = fechaVencimiento.AddDays(1);
                if (fechaVencimiento.DayOfWeek != DayOfWeek.Saturday && fechaVencimiento.DayOfWeek != DayOfWeek.Sunday)
                {
                    diasHabiles++;
                }
            }

            return fechaVencimiento;
        }



        // GET: PrestamoController/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestamo(int id)     // no es necesario eliminarlos, solo darlos de baja (finalizar), tambien puede funcionar como un cancelar
        {
            try
            {
                var prestamo = await _context.Prestamo.FindAsync(id);
                if (prestamo == null)
                {
                    return NotFound();
                }
                _context.Prestamo.Remove(prestamo);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Prestamo eliminado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: PrestamoController/Edit/5
        [HttpPut("Editar/{id}")] // Ruta específica para el método EditarPrestamo
        public async Task<IActionResult> EditarPrestamo(int id, [FromBody] Prestamo prestamo)
        {
            try
            {
                if (prestamo == null)
                {
                    return NotFound();
                }

                var prestamoExistente = await _context.Prestamo
                    .Include(p => p.UsuarioSimple)
                    .Include(p => p.Ejemplar)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (prestamoExistente == null)
                {
                    return BadRequest("El préstamo no existe.");
                }

                prestamoExistente.FechaInicio = prestamo.FechaInicio;
                prestamoExistente.FechaVencimiento = prestamo.FechaVencimiento;

                await _context.SaveChangesAsync();

                return Ok("Préstamo actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT: PrestamoController/Finalizar/5
        //[HttpPut("{prestamoId}")]
        [HttpPut("Finalizar/{prestamoId}")] // Ruta específica para el método FinalizarPrestamo
        public async Task<IActionResult> FinalizarPrestamo(int prestamoId)
        {
            try
            {
                var prestamo = await _context.Prestamo
                    .Include(p => p.UsuarioSimple)
                    .Include(p => p.Ejemplar)
                    .FirstOrDefaultAsync(p => p.Id == prestamoId);

                if (prestamo == null)
                {
                    return NotFound("El préstamo no existe.");
                }

                if (prestamo.FechaFin != DateTime.MinValue)
                {
                    return BadRequest("El préstamo ya ha sido finalizado.");
                }

                prestamo.FechaFin = DateTime.Now;
                prestamo.Ejemplar.Prestado = false;
                // Actualizar los puntajes del usuario, si es necesario

                await _context.SaveChangesAsync();

                return Ok("Préstamo finalizado con éxito.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
