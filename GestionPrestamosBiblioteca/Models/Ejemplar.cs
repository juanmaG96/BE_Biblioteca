using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace GestionPrestamosBiblioteca.Models
{
    public class Ejemplar
    {
        [Key]
        public int CodigoInventario { get; set; }
        [Required]
        public int CodigoUbicacion { get; set; }
        [Required]
        public DateTime FechaAlta { get; set; }
        [Required]
        public bool Prestado { get; set; }
        [JsonIgnore]
        public ICollection<Prestamo> Prestamos { get; set; } // Relación uno a muchos con Prestamo

        public Ejemplar()
        {
            Prestamos = new HashSet<Prestamo>();
        }
    }
}
