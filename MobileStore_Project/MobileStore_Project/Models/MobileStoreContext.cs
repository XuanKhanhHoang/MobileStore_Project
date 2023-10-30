using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MobileStore_Project.Models
{
    public partial class MobileStoreContext : DbContext
    {
        public MobileStoreContext()
        {
        }

        public MobileStoreContext(DbContextOptions<MobileStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<DonHang> DonHangs { get; set; } = null!;
        public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public virtual DbSet<Nsx> Nsxes { get; set; } = null!;
        public virtual DbSet<SanPham> SanPhams { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(local); Initial Catalog=MobileStore; Persist Security Info=True; User ID=sa; Password=123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.Idadmin)
                    .HasName("pk_Admin");

                entity.ToTable("Admin");

                entity.Property(e => e.Idadmin)
                    .HasMaxLength(10)
                    .HasColumnName("IDAdmin")
                    .IsFixedLength();

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Passw).HasMaxLength(50);

                entity.Property(e => e.Role).HasMaxLength(50);
            });

            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.HasKey(e => e.MaDh)
                    .HasName("pk_DonHang");

                entity.ToTable("DonHang");

                entity.Property(e => e.MaDh)
                    .HasMaxLength(10)
                    .HasColumnName("MaDH")
                    .IsFixedLength();

                entity.Property(e => e.GiaTri).HasColumnType("money");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .HasColumnName("ID")
                    .IsFixedLength();

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.ThanhToan).HasMaxLength(50);

                entity.Property(e => e.TinhTrangDh)
                    .HasMaxLength(50)
                    .HasColumnName("TinhTrangDH");

                entity.Property(e => e.VanChuyen).HasMaxLength(50);

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.DonHangs)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DonHang_KhachHang");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.DonHangs)
                    .HasForeignKey(d => d.MaSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DonHang_SanPham");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.ToTable("KhachHang");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .HasColumnName("ID")
                    .IsFixedLength();

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.HoTen).HasMaxLength(50);

                entity.Property(e => e.Passw).HasMaxLength(50);

                entity.Property(e => e.SoDt)
                    .HasMaxLength(50)
                    .HasColumnName("SoDT");
            });

            modelBuilder.Entity<Nsx>(entity =>
            {
                entity.HasKey(e => e.MaNsx)
                    .HasName("pk_NSX");

                entity.ToTable("NSX");

                entity.Property(e => e.MaNsx)
                    .HasMaxLength(10)
                    .HasColumnName("MaNSX")
                    .IsFixedLength();

                entity.Property(e => e.TenNsx).HasColumnName("TenNSX");
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSp)
                    .HasName("pk_SanPham");

                entity.ToTable("SanPham");

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.Property(e => e.AnhSp).HasColumnName("AnhSP");

                entity.Property(e => e.Giaban).HasColumnType("money");

                entity.Property(e => e.MaNsx)
                    .HasMaxLength(10)
                    .HasColumnName("MaNSX")
                    .IsFixedLength();

                entity.Property(e => e.MauSac).HasMaxLength(50);

                entity.Property(e => e.PhienBan).HasMaxLength(50);

                entity.Property(e => e.TenSp).HasColumnName("TenSP");

                entity.HasOne(d => d.MaNsxNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaNsx)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_SanPham_NSX");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
