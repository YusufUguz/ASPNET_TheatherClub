using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TheaterClubProject.Models
{
    public partial class TheaterClubContext : DbContext
    {
        public TheaterClubContext()
        {
        }

        public TheaterClubContext(DbContextOptions<TheaterClubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Loginc> Logincs { get; set; } = null!;
        public virtual DbSet<Sehir> Sehirs { get; set; } = null!;
        public virtual DbSet<Sehirilce> Sehirilces { get; set; } = null!;
        public virtual DbSet<TblGosteriSalonu> TblGosteriSalonus { get; set; } = null!;
        public virtual DbSet<TblGosteriTakip> TblGosteriTakips { get; set; } = null!;
        public virtual DbSet<TblOyun> TblOyuns { get; set; } = null!;
        public virtual DbSet<TblOyuncu> TblOyuncus { get; set; } = null!;
        public virtual DbSet<TblSaAlan> TblSaAlans { get; set; } = null!;
        public virtual DbSet<TblSahneArkasi> TblSahneArkasis { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=HPYUSUF;initial Catalog=TheaterClub;TrustServerCertificate=true;trusted_connection=yes ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sehir>(entity =>
            {
                entity.Property(e => e.SehirId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Sehirilce>(entity =>
            {
                entity.Property(e => e.SehirilceId).ValueGeneratedNever();

                entity.HasOne(d => d.Sehir)
                    .WithMany(p => p.Sehirilces)
                    .HasForeignKey(d => d.SehirId)
                    .HasConstraintName("FK_Sehirilce_Sehir");
            });

            modelBuilder.Entity<TblGosteriSalonu>(entity =>
            {
                entity.Property(e => e.SalonId).ValueGeneratedNever();
            });

            modelBuilder.Entity<TblGosteriTakip>(entity =>
            {
                entity.Property(e => e.GosteriId).ValueGeneratedNever();

                entity.HasOne(d => d.GosteriOyun)
                    .WithMany(p => p.TblGosteriTakips)
                    .HasForeignKey(d => d.GosteriOyunId)
                    .HasConstraintName("FK_TblGosteriTakip_TblOyun");

                entity.HasOne(d => d.GosteriSalon)
                    .WithMany(p => p.TblGosteriTakips)
                    .HasForeignKey(d => d.GosteriSalonId)
                    .HasConstraintName("FK_TblGosteriTakip_TblGosteriSalonu");

                entity.HasOne(d => d.GosteriSehir)
                    .WithMany(p => p.TblGosteriTakips)
                    .HasForeignKey(d => d.GosteriSehirId)
                    .HasConstraintName("FK_TblGosteriTakip_Sehir");

                entity.HasOne(d => d.GosteriSehirilce)
                    .WithMany(p => p.TblGosteriTakips)
                    .HasForeignKey(d => d.GosteriSehirilceId)
                    .HasConstraintName("FK_TblGosteriTakip_Sehirilce");
            });

            modelBuilder.Entity<TblOyun>(entity =>
            {
                entity.Property(e => e.OyunId).ValueGeneratedNever();
            });

            modelBuilder.Entity<TblOyuncu>(entity =>
            {
                entity.Property(e => e.OyuncuId).ValueGeneratedNever();
            });

            modelBuilder.Entity<TblSaAlan>(entity =>
            {
                entity.Property(e => e.SaAlanId).ValueGeneratedNever();
            });

            modelBuilder.Entity<TblSahneArkasi>(entity =>
            {
                entity.Property(e => e.SahneArkasiId).ValueGeneratedNever();

                entity.HasOne(d => d.SaAlan)
                    .WithMany(p => p.TblSahneArkasis)
                    .HasForeignKey(d => d.SaAlanId)
                    .HasConstraintName("FK_TblSahneArkasi_TblSA_Alan");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
