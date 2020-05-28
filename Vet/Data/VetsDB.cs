using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vet.Models;
namespace Vet.Data
{
   
    /// <summary>
    /// Representa a BD
    /// </summary>
    public class VetsDB : IdentityDbContext
    {//equivalente a create database
        /// <summary>
        /// construtor da classe liga a classe a bd
        /// </summary>
        /// <param name="options"></param>
        public VetsDB(DbContextOptions<VetsDB> options) : base(options) { }
        //adicionar á bd
    public virtual DbSet<Animais> Animais { get; set; }
    public virtual DbSet<Donos> Donos { get; set; }
     public virtual DbSet<Veterin> Veterinarios { get; set; }
     public virtual DbSet<Consulta> Consultas { get; set; }


    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        // insert DB seed
    //        modelBuilder.Entity<Donos>().HasData(
    //           new Donos { ID = 1, Nome = "Luís Freitas", Sexo = "M", NIF = "813635582" },
    //           new Donos { ID = 2, Nome = "Andreia Gomes", Sexo = "F", NIF = "854613462" },
    //           new Donos { ID = 3, Nome = "Cristina Sousa", Sexo = "F", NIF = "265368715" },
    //           new Donos { ID = 4, Nome = "Sónia Rosa", Sexo = "F", NIF = "835623190" },
    //           new Donos { ID = 5, Nome = "António Santos", Sexo = "M", NIF = "751512205" },
    //           new Donos { ID = 6, Nome = "Gustavo Alves", Sexo = "M", NIF = "728663835" },
    //           new Donos { ID = 7, Nome = "Rosa Vieira", Sexo = "F", NIF = "644388118" },
    //           new Donos { ID = 8, Nome = "Daniel Dias", Sexo = "M", NIF = "262618487" },
    //           new Donos { ID = 9, Nome = "Tânia Gomes", Sexo = "F", NIF = "842615197" },
    //           new Donos { ID = 10, Nome = "Andreia Correia", Sexo = "F", NIF = "635139506" }
    //            );

    //    }

    }
}


