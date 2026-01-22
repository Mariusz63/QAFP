using Microsoft.EntityFrameworkCore;
using QAFPform.Models;
using QAFPform.Models.Enums;

namespace QAFPform.Data
{
    public class QAFPformDbContext : DbContext
    {
        public QAFPformDbContext(DbContextOptions<QAFPformDbContext> options)
            : base(options)
        {
        }

        public DbSet<Formularz> FormularzViews { get; set; }
        public DbSet<Adres> Adres { get; set; }
        public DbSet<ProdukcjaItem> ProdukcjaItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================================
            // Relacje Adres -> FormularzViewModel (jeden-do-jednego)
            // ==========================================
            modelBuilder.Entity<Formularz>()
                .HasOne(f => f.AdresKorespondencyjny)
                .WithOne()
                .HasForeignKey<Adres>(a => a.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Formularz>()
                .HasOne(f => f.AdresSiedziby)
                .WithOne()
                .HasForeignKey<Adres>(a => a.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Formularz>()
                .HasOne(f => f.ZakladuLubGospodarstwa)
                .WithOne()
                .HasForeignKey<Adres>(a => a.Id)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // Relacja FormularzViewModel -> ProdukcjaItem (jeden-do-wielu)
            // ==========================================
            modelBuilder.Entity<Formularz>()
                .HasMany(f => f.Produkcje)
                .WithOne(p => p.Formularz)
                .HasForeignKey(p => p.FormularzId)
                .OnDelete(DeleteBehavior.Cascade);

            // ==========================================
            // Konfiguracja pól dodatkowych jeśli potrzeba
            // ==========================================
            //modelBuilder.Entity<FormularzViewModel>()
            //    .Property(f => f.MiejsceZlozenia)
            //    .IsRequired();

            //modelBuilder.Entity<FormularzViewModel>()
            //    .Property(f => f.DataZlozenia)
            //    .HasDefaultValueSql("getdate()");
        }
    }
}
