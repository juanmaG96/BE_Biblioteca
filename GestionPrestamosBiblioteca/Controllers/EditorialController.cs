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

    public class EditorialController : ControllerBase

    {
        private readonly AplicationDbContext _context;

        public EditorialController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: EditorialController
        [HttpGet]
        public async Task<IActionResult> GetListaEditoriales()
        {
            try
            {
                var listEditoriales = await _context.Editorial.ToListAsync();
                return Ok(listEditoriales);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: EditorialController/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEditorial(int id)
        {
            try
            {
                var editorial = await _context.Editorial.FindAsync(id);
                if (editorial == null)
                {
                    return NotFound();
                }
                return Ok(editorial);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: EditorialController/Create
        [HttpPost]
        public async Task<IActionResult> RegistrarEditorial([FromBody] Editorial editorial)
        {
            try
            {
                _context.Add(editorial);
                await _context.SaveChangesAsync();
                return Ok(editorial);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: EditorialController/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEditorial(int id)
        {
            try
            {
                var editorial = await _context.Editorial.FindAsync(id);
                if (editorial == null)
                {
                    return NotFound();
                }
                _context.Editorial.Remove(editorial);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Editorial eliminada con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: EditorialController/Edit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarEditorial(int id, [FromBody] Editorial editorial)
        {
            try
            {
                if (id != editorial.Id)
                {
                    return BadRequest();
                }
                _context.Update(editorial);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Editorial actualizada con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
