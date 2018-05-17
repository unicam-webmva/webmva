using webmva.Models;
using Microsoft.EntityFrameworkCore;

namespace webmva.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        
        //QUI CI VANNO LE TABELLE DEL DB
        public DbSet<Modulo> Moduli { get; set; }
        
        public DbSet<Progetto> Progetti { get; set; }
        public DbSet<ModuliProgetto> ModuliProgetto {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<ModuloNMAP>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloNESSUS>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloDNSRECON>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloDROOPE>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloINFOGA>().HasBaseType<Modulo>();
        }
        }
    }
}