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

    public class LibroAutorController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public LibroAutorController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: LibroAutorController
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listLibroAutores = await _context.LibroAutor.ToListAsync();
                return Ok(listLibroAutores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: LibroAutorController/Details/5
        [HttpGet("{libroId}/{autorId}")]
        public async Task<IActionResult> Get(int libroId, int autorId)
        {
            try
            {
                var libroAutor = await _context.LibroAutor.FirstOrDefaultAsync(la => la.LibroId == libroId && la.AutorId == autorId);

                if (libroAutor == null)
                {
                    return NotFound();
                }

                return Ok(libroAutor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST:  LibroAutorController/Create
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LibroAutor libroAutor)
        {
            try
            {
                _context.Add(libroAutor);
                await _context.SaveChangesAsync();
                return Ok(libroAutor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: LibroAutorController/Delete/5
        [HttpDelete("{libroId}/{autorId}")]
        public async Task<IActionResult> Delete(int libroId, int autorId)
        {
            try
            {
                var libroAutor = await _context.LibroAutor.FirstOrDefaultAsync(la => la.LibroId == libroId && la.AutorId == autorId);
                if (libroAutor == null)
                {
                    return NotFound();
                }
                _context.LibroAutor.Remove(libroAutor);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Eliminado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: LibroAutorController/Edit/5
        [HttpPut("{libroId}/{autorId}")]
        public async Task<IActionResult> Put(int libroId, int autorId, [FromBody] LibroAutor libroAutor)
        {
            try
            {
                if (libroAutor == null || libroAutor.LibroId != libroId || libroAutor.AutorId != autorId)
                {
                    return BadRequest();
                }

                _context.Update(libroAutor);
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