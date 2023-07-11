using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace GestionPrestamosBiblioteca.Models
{
    public class Prestamo
    {
        public int Id { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        
        public DateTime FechaFin { get; set; }
        public DateTime FechaVencimiento { get; set; }

        // Relaciones con otras entidades
        public int EjemplarId { get; set; }
        [JsonIgnore]
        public Ejemplar Ejemplar { get; set; }
        public int UsuarioId { get; set; }
        [JsonIgnore]
        public UsuarioSimple UsuarioSimple { get; set; }
    }
}
