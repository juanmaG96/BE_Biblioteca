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

    public class EjemplarController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public EjemplarController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: EjemplarController
        [HttpGet]
        public async Task<IActionResult> GetListaEjemplares()
        {
            try
            {
                var listEjemplares = await _context.Ejemplar.ToListAsync();
                return Ok(listEjemplares);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: EjemplarController/Details/5
        [HttpGet("{codigoInventario}")]
        public async Task<IActionResult> GetEjemplar(int codigoInventario)
        {
            try
            {
                var ejemplar = await _context.Ejemplar.FindAsync(codigoInventario);
                if (ejemplar == null)
                {
                    return NotFound();
                }
                return Ok(ejemplar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST:EjemplarController/Create
        [HttpPost]
        public async Task<IActionResult> RegistrarEjemplar(int isbn, [FromBody] Ejemplar ejemplar)
        {
            try
            {
                var libro = await _context.Libro.FindAsync(isbn);
                if (libro == null)
                {
                    return BadRequest("Libro no encontrado.");
                }

                var nuevoEjemplar = new Ejemplar 
                {
                    CodigoInventario = ejemplar.CodigoInventario,
                    CodigoUbicacion = ejemplar.CodigoUbicacion,
                    FechaAlta = ejemplar.FechaAlta,
                    Prestado = false
                };

                libro.Ejemplares.Add(nuevoEjemplar); // Agregar el nuevo ejemplar a la colección de ejemplares de la clase libros

                _context.Add(nuevoEjemplar);
                await _context.SaveChangesAsync();
                return Ok(nuevoEjemplar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: EjemplarController/Delete/5
        [HttpDelete("{codigoInventario}")]
        public async Task<IActionResult> DeleteEjemplar(int codigoInventario)
        {
            try
            {
                var ejemplar = await _context.Ejemplar.FindAsync(codigoInventario);
                if (ejemplar == null)
                {
                    return NotFound();
                }

                var libro = await _context.Libro.Include(l => l.Ejemplares)
                                   .FirstOrDefaultAsync(l => l.Ejemplares.Contains(ejemplar));

                if (libro != null)
                {
                    libro.Ejemplares.Remove(ejemplar);      // Elimina el ejemplar de la propiedad ejemplares

                }
               
                _context.Ejemplar.Remove(ejemplar);

                await _context.SaveChangesAsync();
                return Ok(new { message = "Ejemplar eliminado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: EjemplarController/Edit/5
        [HttpPut("{codigoInventario}")]
        public async Task<IActionResult> EditarEjemplar(int codigoInventario, [FromBody] Ejemplar ejemplar)
        {
            try
            {
                if (codigoInventario != ejemplar.CodigoInventario)
                {
                    return BadRequest();
                }

                var libro = await _context.Libro.Include(l => l.Ejemplares)
                                            .FirstOrDefaultAsync(l => l.Ejemplares.Any(e => e.CodigoInventario == codigoInventario));

                if (libro != null)
                {
                    var ejemplarExistente = libro.Ejemplares.FirstOrDefault(e => e.CodigoInventario == codigoInventario);

                    if (ejemplarExistente != null)
                    {
                        // Actualizar propiedades del ejemplar existente con los valores del nuevo ejemplar
                        ejemplarExistente.CodigoUbicacion = ejemplar.CodigoUbicacion;
                        ejemplarExistente.FechaAlta = ejemplar.FechaAlta;
                        ejemplarExistente.Prestado = ejemplar.Prestado;
                    }
                }

                await _context.SaveChangesAsync();

                return Ok(new { message = "Ejemplar actualizado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }

}