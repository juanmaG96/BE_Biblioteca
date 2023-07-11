﻿// <auto-generated />
using System;
using GestionPrestamosBiblioteca;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GestionPrestamosBiblioteca.Migrations
{
    [DbContext(typeof(AplicationDbContext))]
    partial class AplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.Autor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Autor");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.Editorial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Editorial");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.Ejemplar", b =>
                {
                    b.Property<int>("CodigoInventario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CodigoUbicacion")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LibroISBN")
                        .HasColumnType("int");

                    b.Property<bool>("Prestado")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("CodigoInventario");

                    b.HasIndex("LibroISBN");

                    b.ToTable("Ejemplar");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.Libro", b =>
                {
                    b.Property<int>("ISBN")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CantidadPaginas")
                        .HasColumnType("int");

                    b.Property<int?>("EditorialId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaPublicacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ISBN");

                    b.HasIndex("EditorialId");

                    b.ToTable("Libro");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.LibroAutor", b =>
                {
                    b.Property<int>("LibroId")
                        .HasColumnType("int");

                    b.Property<int>("AutorId")
                        .HasColumnType("int");

                    b.HasKey("LibroId", "AutorId");

                    b.HasIndex("AutorId");

                    b.ToTable("LibroAutor");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.LibroCategoria", b =>
                {
                    b.Property<int>("LibroId")
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.HasKey("LibroId", "CategoriaId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("LibroCategoria");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.Prestamo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("EjemplarId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaVencimiento")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioSimpleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EjemplarId");

                    b.HasIndex("UsuarioSimpleId");

                    b.ToTable("Prestamo");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.UsuarioAdministrador", b =>
                {
                    b.Property<string>("NombreUsuario")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Contrasena")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("NombreUsuario");

                    b.ToTable("UsuarioAdministrador");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.UsuarioSimple", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Documento")
                        .HasColumnType("int");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Puntaje")
                        .HasColumnType("int");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UsuarioSimple");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.Ejemplar", b =>
                {
                    b.HasOne("GestionPrestamosBiblioteca.Models.Libro", null)
                        .WithMany("Ejemplares")
                        .HasForeignKey("LibroISBN");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.Libro", b =>
                {
                    b.HasOne("GestionPrestamosBiblioteca.Models.Editorial", null)
                        .WithMany("Libros")
                        .HasForeignKey("EditorialId");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.LibroAutor", b =>
                {
                    b.HasOne("GestionPrestamosBiblioteca.Models.Autor", "Autor")
                        .WithMany("LibroAutores")
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionPrestamosBiblioteca.Models.Libro", "Libro")
                        .WithMany("LibroAutores")
                        .HasForeignKey("LibroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Autor");

                    b.Navigation("Libro");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.LibroCategoria", b =>
                {
                    b.HasOne("GestionPrestamosBiblioteca.Models.Categoria", "Categoria")
                        .WithMany("LibroCategorias")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionPrestamosBiblioteca.Models.Libro", "Libro")
                        .WithMany("LibroCategorias")
                        .HasForeignKey("LibroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Libro");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.Prestamo", b =>
                {
                    b.HasOne("GestionPrestamosBiblioteca.Models.Ejemplar", "Ejemplar")
                        .WithMany("Prestamos")
                        .HasForeignKey("EjemplarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionPrestamosBiblioteca.Models.UsuarioSimple", "UsuarioSimple")
                        .WithMany("Prestamos")
                        .HasForeignKey("UsuarioSimpleId");

                    b.Navigation("Ejemplar");

                    b.Navigation("UsuarioSimple");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.Autor", b =>
                {
                    b.Navigation("LibroAutores");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.Categoria", b =>
                {
                    b.Navigation("LibroCategorias");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.Editorial", b =>
                {
                    b.Navigation("Libros");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.Ejemplar", b =>
                {
                    b.Navigation("Prestamos");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.Libro", b =>
                {
                    b.Navigation("Ejemplares");

                    b.Navigation("LibroAutores");

                    b.Navigation("LibroCategorias");
                });

            modelBuilder.Entity("GestionPrestamosBiblioteca.Models.UsuarioSimple", b =>
                {
                    b.Navigation("Prestamos");
                });
#pragma warning restore 612, 618
        }
    }
}
