using GestionPrestamosBiblioteca.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionPrestamosBiblioteca
{
    public class AplicationDbContext: DbContext
    {
        public DbSet<UsuarioSimple> UsuarioSimple { get; set; }
        public DbSet<UsuarioAdministrador> UsuarioAdministrador { get; set; }
        public DbSet<Autor> Autor { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Editorial> Editorial { get; set; }
        public DbSet<Ejemplar> Ejemplar { get; set; }
        public DbSet<Libro> Libro { get; set; }
        public DbSet<LibroAutor> LibroAutor { get; set; }
        public DbSet<LibroCategoria> LibroCategoria { get; set; }
        public DbSet<Prestamo> Prestamo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibroCategoria>()
                        .HasKey(la => new { la.LibroId, la.CategoriaId });

            modelBuilder.Entity<LibroAutor>()
                        .HasKey(la => new { la.LibroId, la.AutorId });
        }
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
    }
}
