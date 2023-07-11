using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionPrestamosBiblioteca.Models
{
    public class UsuarioAdministrador
    {
        [Key]
        public string NombreUsuario { get; set; }
        [Required]
        public string Contrasena { get; set; }
    }
}
