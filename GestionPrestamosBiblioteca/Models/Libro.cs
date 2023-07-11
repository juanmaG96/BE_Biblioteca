using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace GestionPrestamosBiblioteca.Models
{
    public class Libro
    {
        [Key]
        public int ISBN { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public DateTime FechaPublicacion { get; set; }
        [Required]
        public int CantidadPaginas { get; set; }
        [JsonIgnore]
        public ICollection<Ejemplar> Ejemplares { get; set; }
        [JsonIgnore]
        public ICollection<LibroAutor> LibroAutores { get; set; }
        [JsonIgnore]
        public ICollection<LibroCategoria> LibroCategorias { get; set; }

        public Libro()
        {
            Ejemplares = new HashSet<Ejemplar>();
            LibroAutores = new HashSet<LibroAutor>();
            LibroCategorias = new HashSet<LibroCategoria>();
        }
    }
}

