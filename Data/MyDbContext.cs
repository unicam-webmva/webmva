using webmva.Models;
using Microsoft.EntityFrameworkCore;

namespace webmva.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        
        //QUI CI VANNO LE TABELLE DEL DB
        public DbSet<Modulo> Moduli { get; set; }
        //public DbSet<Progetto> Progetti { get; set; }
        //public DbSet<Risultato> Risultati { get; set; }
    }
}