using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestionPrestamosBiblioteca.Models
{
    public class UsuarioSimple
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public int Telefono { get; set; }
        [Required]
        public int Documento { get; set; }
        // [Required]
        public int Puntaje { get; set; }
        [Required]
        public string Mail { get; set; }
        [JsonIgnore]
        public ICollection<Prestamo> Prestamos { get; set; } // Relación uno a muchos con Prestamo

        public UsuarioSimple()
        {
            Prestamos = new HashSet<Prestamo>();
        }
    }
}
