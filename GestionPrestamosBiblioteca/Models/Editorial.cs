using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace GestionPrestamosBiblioteca.Models
{
    public class Editorial
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public int Telefono { get; set; }
        [JsonIgnore]
        public ICollection<Libro> Libros { get; set; } // Relación uno a muchos con Libro

        public Editorial()
        {
            Libros = new HashSet<Libro>();
        }
    }
}
