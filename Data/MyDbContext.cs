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
        public DbSet<ModuliProgetto> ModuliProgetto { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<PercorsiReport> PercorsiReport { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModuloNMAP>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloNESSUS>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloDNSRECON>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloFIERCE>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloDROOPE>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloJOOMSCAN>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloWPSCAN>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloINFOGA>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloOPENDOOR>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloWASCAN>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloINFOGAEMAIL>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloINFOGAEMAIL>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloWAPITI>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloSQLMAP>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloWIFITE>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloSUBLIST3R>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloNOSQL>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloODAT>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloDNSENUM>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloOPENVAS>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloTHEHARVESTER>().HasBaseType<Modulo>();
            modelBuilder.Entity<ModuloAMASS>().HasBaseType<Modulo>();

        }
    }
}
