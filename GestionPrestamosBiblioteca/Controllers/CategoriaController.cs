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

    public class CategoriaController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public CategoriaController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: CategoriaController
        [HttpGet]
        public async Task<IActionResult> GetListaCategorias()
        {
            try
            {
                var listCategoria = await _context.Categoria.ToListAsync();
                return Ok(listCategoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: CategoriaController/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoria(int id)
        {
            try
            {
                var categoria = await _context.Categoria.FindAsync(id);
                if (categoria == null)
                {
                    return NotFound();
                }
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: CategoriaController/Create
        [HttpPost]
        public async Task<IActionResult> RegistrarCategoria([FromBody] Categoria categoria)
        {
            try
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: CategoriaController/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            try
            {
                var categoria = await _context.Categoria.FindAsync(id);
                if (categoria == null)
                {
                    return NotFound();
                }
                var libroCategoria = await _context.LibroCategoria.Include(l => l.Categoria)
                                         .FirstOrDefaultAsync(l => l.Categoria == categoria);
                if (libroCategoria != null)
                {
                    return BadRequest("No se puede eliminar la categoria, contiene libros");
                }
                else
                {
                    _context.Categoria.Remove(categoria);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Categoria eliminada con exito" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: CategoriaController/Edit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCategoria(int id, [FromBody] Categoria categoria)
        {
            try
            {
                if (id != categoria.Id)
                {
                    return BadRequest();
                }
                var categoriaExistente = await _context.Categoria.Include(l => l.LibroCategorias)
                                                .FirstOrDefaultAsync(l => l.Id == id);

                if (categoriaExistente != null)
                {
                    var listaLibros = categoriaExistente.LibroCategorias.Select(lc => lc.Categoria).ToList();

                    categoriaExistente.Nombre = categoria.Nombre;

                    foreach (var libroCategoria in categoriaExistente.LibroCategorias)
                    {
                        libroCategoria.Categoria = categoriaExistente;
                    }
                }
            
                await _context.SaveChangesAsync();
                return Ok(new { message = "Categoria actualizada con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}