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

    public class LibroCategoriaController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public LibroCategoriaController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: LibroCategoriaController
        [HttpGet]
        public async Task<IActionResult> GetListaLibrosCategorias()
        {
            try
            {
                var listLibroCategorias = await _context.LibroCategoria.ToListAsync();
                return Ok(listLibroCategorias);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: LibroCategoriaController/Details/5
        [HttpGet("{libroId}/{categoriaId}")]
        public async Task<IActionResult> GetLibroCategoria(int libroId, int categoriaId)
        {
            try
            {
                var libroCategoria = await _context.LibroCategoria.FirstOrDefaultAsync(la => la.LibroId == libroId && la.CategoriaId == categoriaId);

                if (libroCategoria == null)
                {
                    return NotFound();
                }

                return Ok(libroCategoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST:  LibroCategoriaController/Create
        [HttpPost]
        public async Task<IActionResult> RegistrarLibroCategoria([FromBody] LibroCategoria libroCategoria)
        {
            try
            {
                _context.Add(libroCategoria);
                await _context.SaveChangesAsync();
                return Ok(libroCategoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: LibroAutorController/Delete/5
        [HttpDelete("{libroId}/{categoriaId}")]
        public async Task<IActionResult> DeleteLibroCategoria(int libroId, int categoriaId)
        {
            try
            {
                var libroCategoria = await _context.LibroCategoria.FirstOrDefaultAsync(la => la.LibroId == libroId && la.CategoriaId == categoriaId);
                if (libroCategoria == null)
                {
                    return NotFound();
                }
                _context.LibroCategoria.Remove(libroCategoria);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Eliminado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: LibroCategoriaController/Edit/5
        [HttpPut("{libroId}/{categoriaId}")]
        public async Task<IActionResult> EditarLibroCategoria(int libroId, int categoriaId, [FromBody] LibroCategoria libroCategoria)
        {
            try
            {
                if (libroCategoria == null || libroCategoria.LibroId != libroId || libroCategoria.CategoriaId != categoriaId)
                {
                    return BadRequest();
                }

                _context.Update(libroCategoria);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Actualizado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}