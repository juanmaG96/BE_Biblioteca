using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace GestionPrestamosBiblioteca.Models
{
    public class LibroCategoria
    {
        
        public int LibroId { get; set; }
        [JsonIgnore]
        public virtual Libro Libro { get; set; }
        public int CategoriaId { get; set; }
        [JsonIgnore]
        public virtual Categoria Categoria { get; set; }
    }
}
