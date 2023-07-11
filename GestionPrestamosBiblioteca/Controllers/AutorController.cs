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

    public class AutorController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public AutorController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: AutorController
        [HttpGet]
        public async Task<IActionResult> GetListaAutores()
        {
            try
            {
                var listAutores = await _context.Autor.ToListAsync();
                return Ok(listAutores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: AutorController/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAutor(int id)
        {
            try
            {
                var autor = await _context.Autor.FindAsync(id);
                if (autor == null)
                {
                    return NotFound();
                }
                return Ok(autor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: AutorController/Create
        [HttpPost]
        public async Task<IActionResult> RegistrarAutor([FromBody] Autor autor)
        {
            try
            {
                _context.Add(autor);
                await _context.SaveChangesAsync();
                return Ok(autor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: AutorController/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor(int id)
        {
            try
            {
                var autor = await _context.Autor.FindAsync(id);
                if (autor == null)
                {
                    return NotFound();
                }
                // var libroAutor = await _context.LibroAutor.Include(l => l.Autor)
                //                .FirstOrDefaultAsync(l => l.Autor.Contains(autor));   // NO SE PUEDE USAR CONTAINS PORQUE AUTOR NO ES UNA COLECCION EN LIBROAUTOR
                
                var libroAutor = await _context.LibroAutor.Include(l => l.Autor)
                                         .FirstOrDefaultAsync(l => l.Autor == autor);   // verifica si el autor de LibroAutor coincide con un valor determinado


                if (libroAutor != null)
                {
                    return BadRequest("No se puede eliminar el autor, contiene libros");
                }
                else
                {
                    _context.Autor.Remove(autor);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Autor eliminado con exito" });
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: AutorController/Edit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarAutor(int id, [FromBody] Autor autor)
        {
            try
            {
                if (id != autor.Id)
                {
                    return BadRequest();
                }
                var autorExistente = await _context.Autor.Include(l => l.LibroAutores)
                                                .FirstOrDefaultAsync(l => l.Id == id);

                if (autorExistente != null)
                {
                    autorExistente.Nombre = autor.Nombre;

                    foreach (var libroAutores in autorExistente.LibroAutores)
                    {
                        libroAutores.Autor = autorExistente;
                    }
                }

                await _context.SaveChangesAsync();
                return Ok(new { message = "Autor actualizado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}