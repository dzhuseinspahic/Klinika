using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjektniZadatak.Models;

namespace ProjektniZadatak.Data
{
    public class KlinikaContext : DbContext
    {
        public KlinikaContext(DbContextOptions<KlinikaContext> options) : base(options)
        {
        }

        public DbSet<Pacijent> Pacijenti { get; set; }
        public DbSet<Ljekar> Ljekari { get; set; }
        public DbSet<Prijem> Prijemi { get; set; }
        public DbSet<Nalaz> Nalazi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //to have singular table names
            modelBuilder.Entity<Pacijent>().ToTable("Pacijent");
            modelBuilder.Entity<Ljekar>().ToTable("Ljekar");
            modelBuilder.Entity<Prijem>().ToTable("Prijem");
            modelBuilder.Entity<Nalaz>().ToTable("Nalaz");

            modelBuilder.Entity<Prijem>()
                .HasOne(p => p.Nalaz)
                .WithOne(n => n.Prijem)
                .HasForeignKey<Nalaz>(n => n.PrijemID);
        }
    }
}
