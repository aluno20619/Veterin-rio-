using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vet.Models;


namespace Vet.Data
{
    /// <summary>
    /// Representa a BD
    /// </summary>
    public class VetsDB : DbContext
    {//equivalente a create database
        /// <summary>
        /// construtor da classe liga a classe a bd
        /// </summary>
        /// <param name="options"></param>
        public VetsDB(DbContextOptions<VetsDB> options) : base(options) { }
        //adicionar á bd
        public DbSet<Animais> Animais { get; set; }
        public DbSet<Donos> Donos { get; set; }
        public DbSet<Veterin> Veterinarios { get; set; }
        public DbSet<Consulta> Consultas { get; set; }

    }
}
