using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace GestionPrestamosBiblioteca.Models
{
    public class Autor
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [JsonIgnore]
        public ICollection<LibroAutor> LibroAutores { get; set; }

        public Autor()
        {
            LibroAutores = new HashSet<LibroAutor>();
        }
    }
}
