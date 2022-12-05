using Microsoft.EntityFrameworkCore;
using Polly.Core.Entities.ML;

namespace Polly.Infrastructure.Data
{
    public partial class PollyContext : DbContext
    {
        public PollyContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=arduino;User Id=arduino;Password=root;");
            }
        }

        public PollyContext(DbContextOptions<PollyContext> options)  : base(options) { }

        public virtual DbSet<emergencia_resumen> Emergencia_Resumens { get; set; }
        public virtual DbSet<emergencia_detalle> Emergencia_Detalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {          

            modelBuilder.Entity<emergencia_resumen>(entity =>
            {
                entity.HasKey(e => e.id)
                   .HasName("emergencia_resumen_pk");

                entity.ToTable("emergencia_resumen");

                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.idFechaHora).HasColumnName("idFechaHora");

                entity.Property(e => e.Ambulancia).HasColumnName("Ambulancia").HasColumnType("decimal(6, 5)");
                entity.Property(e => e.Bomberos).HasColumnName("Bomberos").HasColumnType("decimal(6, 5)");
                entity.Property(e => e.Emergencia).HasColumnName("Emergencia").HasColumnType("decimal(6, 5)");
                entity.Property(e => e.Policia).HasColumnName("Policia").HasColumnType("decimal(6, 5)");
                entity.Property(e => e.Ruido).HasColumnName("Ruido").HasColumnType("decimal(6, 5)");
                entity.Property(e => e.Transito).HasColumnName("Transito").HasColumnType("decimal(6, 5)");
                entity.Property(e => e.MoyorLabel)
                    .HasColumnName("MoyorLabel")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsRequired();
                entity.Property(e => e.MayorValor).HasColumnName("MayorValor").HasColumnType("decimal(6, 5)");
                entity.Property(e => e.fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.hora)
                    .HasColumnName("hora")
                    .HasColumnType("time");
            });

            modelBuilder.Entity<emergencia_detalle>(entity =>
            {
                entity.HasKey(e => e.id)
                   .HasName("emergencia_detalle_pk");

                entity.ToTable("emergencia_detalle");

                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.idFechaHora).HasColumnName("idFechaHora");

                entity.Property(e => e.organinismo)
                    .HasColumnName("organinismo")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsRequired();
                                
                entity.Property(e => e.valor).HasColumnName("valor").HasColumnType("decimal(6, 5)");
        
                entity.Property(e => e.fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.hora)
                    .HasColumnName("hora")
                    .HasColumnType("time");
            });

        }


    }
}
