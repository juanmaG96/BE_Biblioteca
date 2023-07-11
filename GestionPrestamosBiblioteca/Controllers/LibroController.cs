using GestionPrestamosBiblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestionPrestamosBiblioteca.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class LibroController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public LibroController(AplicationDbContext context)
        {
            _context = context;
        }


        // GET: LibroController
        [HttpGet]
        public async Task<IActionResult> GetListaLibros()
        {
            try
            {
                var listLibros = await _context.Libro.ToListAsync();
                return Ok(listLibros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: LibroController/Details/5
        [HttpGet("{isbn}")]
        public async Task<IActionResult> GetLibro(int isbn)
        {
            try
            {
                var libro = await _context.Libro.FindAsync(isbn);
                if (libro == null)
                {
                    return NotFound();
                }
                return Ok(libro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: LibroController/Create
        [HttpPost]
        public async Task<IActionResult> RegistrarLibro(int idEditorial, int idCategoria, int idAutor, [FromBody] Libro libro)
        {
            try
            {
                var editorial = await _context.Editorial.FindAsync(idEditorial);
                var categoria = await _context.Categoria.FindAsync(idCategoria);
                var autor = await _context.Autor.FindAsync(idAutor);
                if (editorial == null)
                {
                    return BadRequest("Editorial no encontrada.");
                }
                if (categoria == null)
                {
                    return BadRequest("Categoria no encontrada.");
                }
                if (autor == null)
                {
                    return BadRequest("Autor no encontrado.");
                }

                var nuevoLibro = new Libro
                {
                    ISBN = libro.ISBN,
                    Titulo = libro.Titulo,
                    FechaPublicacion = libro.FechaPublicacion,
                    CantidadPaginas = libro.CantidadPaginas
                }; 

                var isbn = nuevoLibro.ISBN;

                var libroExiste = await _context.Libro.FindAsync(isbn);
                if (libroExiste != null)
                {
                    return BadRequest("ISBN ya está registrado");
                }
                
                var libroAutor = new LibroAutor
                {
                    LibroId = nuevoLibro.ISBN,
                    Libro = nuevoLibro,
                    AutorId = idAutor,
                    Autor = autor
                };

                var libroCategoria = new LibroCategoria
                {
                    LibroId = nuevoLibro.ISBN,
                    Libro = nuevoLibro,
                    CategoriaId = idCategoria,
                    Categoria = categoria
                };

                nuevoLibro.LibroAutores = null;
                nuevoLibro.LibroCategorias = null;
                /*
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                string jsonString = JsonConvert.SerializeObject(nuevoLibro, settings);
                */
                

                // Resto del código para guardar el nuevo libro y las relaciones en la base de datos...

                _context.LibroAutor.Add(libroAutor);
                _context.LibroCategoria.Add(libroCategoria);
                editorial.Libros.Add(nuevoLibro);  // Agregar el nuevo libro a la colección de libros de la editorial
                categoria.LibroCategorias.Add(libroCategoria);
                autor.LibroAutores.Add(libroAutor);
                nuevoLibro.LibroAutores.Add(libroAutor);
                nuevoLibro.LibroCategorias.Add(libroCategoria);

                

                _context.Add(nuevoLibro);
                await _context.SaveChangesAsync();
                return Ok(nuevoLibro);
                //return Ok(jsonString);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        // GET: LibroController/Delete/5
        [HttpDelete("{isbn}")]
        public async Task<IActionResult> DeleteLibro(int isbn)
        {
            try
            {
                var libro = await _context.Libro.FindAsync(isbn);
                if (libro == null)
                {
                    return NotFound();
                }
                var libroCategoria = await _context.LibroCategoria.FirstOrDefaultAsync(la => la.LibroId == isbn);
                var libroAutor = await _context.LibroAutor.FirstOrDefaultAsync(la => la.LibroId == isbn);

                /*
                var libroAutores = await _context.LibroAutor.Where(la => la.LibroId == isbn).ToListAsync();

                if (libroAutores.Count > 0)
                {
                    // Se encontraron uno o más registros de LibroAutor con el ISBN especificado
                    _context.LibroAutor.RemoveRange(libroAutores);
                    await _context.SaveChangesAsync();
                    // Realizar cualquier otra operación necesaria después de eliminar los registros
                }
                */

                _context.LibroCategoria.Remove(libroCategoria);
                _context.LibroAutor.Remove(libroAutor);     // Al hacer el removeRange de libroAutor  libroCategoria, estas lineas no serian necesarias
                _context.Libro.Remove(libro);       // esta si
                await _context.SaveChangesAsync();
                return Ok(new { message = "Libro eliminado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        // PUT: LibroController/Edit/5
        
        [HttpPut("{isbn}")]
        public async Task<IActionResult> EditarLibro(int isbn, [FromBody] Libro libro)
        {
            try
            {
                if (isbn != libro.ISBN)
                {
                    return BadRequest();
                }

                var libroExistente = await _context.Libro.Include(l => l.LibroCategorias)
                                                        .Include(l => l.LibroAutores)
                                                        .FirstOrDefaultAsync(l => l.ISBN == isbn);

                if (libroExistente != null)
                {
                    // Crear una lista separada para cada autor y categoría del libro
                    var listaAutores = libroExistente.LibroAutores.Select(la => la.Autor).ToList();
                    var listaCategorias = libroExistente.LibroCategorias.Select(lc => lc.Categoria).ToList();

                    // Actualizar propiedades del libro existente con los valores del nuevo libro
                    libroExistente.Titulo = libro.Titulo;
                    libroExistente.FechaPublicacion = libro.FechaPublicacion;
                    libroExistente.CantidadPaginas = libro.CantidadPaginas;

                    // Actualizar las listas de autores y categorías del libro
                    libroExistente.LibroAutores = listaAutores.Select(a => new LibroAutor { Autor = a, AutorId = a.Id }).ToList();
                    libroExistente.LibroCategorias = listaCategorias.Select(c => new LibroCategoria { Categoria = c, CategoriaId = c.Id }).ToList();
                }

                await _context.SaveChangesAsync();

                return Ok(new { message = "Libro actualizado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        


    }
}
