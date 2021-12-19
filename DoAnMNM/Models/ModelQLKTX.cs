using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DoAnMNM.Models
{
    public partial class ModelQLKTX : DbContext
    {
        public ModelQLKTX()
            : base("name=ModelQLKTX")
        {
        }

        public virtual DbSet<DonDangKy> DonDangKies { get; set; }
        public virtual DbSet<HoaDonDien> HoaDonDiens { get; set; }
        public virtual DbSet<HoaDonTienPhong> HoaDonTienPhongs { get; set; }
        public virtual DbSet<Khu> Khus { get; set; }
        public virtual DbSet<LoaiPhong> LoaiPhongs { get; set; }
        public virtual DbSet<Phong> Phongs { get; set; }
        public virtual DbSet<QuanLy> QuanLies { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonDangKy>()
                .Property(e => e.MaDonDangKy)
                .IsUnicode(false);

            modelBuilder.Entity<DonDangKy>()
                .Property(e => e.MaQL)
                .IsUnicode(false);

            modelBuilder.Entity<DonDangKy>()
                .Property(e => e.MSSV)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonDien>()
                .Property(e => e.MaQL)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonDien>()
                .Property(e => e.MaPhong)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonDien>()
                .Property(e => e.DonGiaDien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HoaDonTienPhong>()
                .Property(e => e.MaQL)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonTienPhong>()
                .Property(e => e.MSSV)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonTienPhong>()
                .Property(e => e.ThanhTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Khu>()
                .Property(e => e.MaKhu)
                .IsUnicode(false);

            modelBuilder.Entity<Khu>()
                .HasMany(e => e.Phongs)
                .WithRequired(e => e.Khu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiPhong>()
                .Property(e => e.MaLoaiPhong)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiPhong>()
                .HasMany(e => e.Phongs)
                .WithRequired(e => e.LoaiPhong)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Phong>()
                .Property(e => e.MaPhong)
                .IsUnicode(false);

            modelBuilder.Entity<Phong>()
                .Property(e => e.MaLoaiPhong)
                .IsUnicode(false);

            modelBuilder.Entity<Phong>()
                .Property(e => e.MaQL)
                .IsUnicode(false);

            modelBuilder.Entity<Phong>()
                .Property(e => e.MaKhu)
                .IsUnicode(false);

            modelBuilder.Entity<Phong>()
                .Property(e => e.MaTruongPhong)
                .IsUnicode(false);

            modelBuilder.Entity<Phong>()
                .HasMany(e => e.HoaDonDiens)
                .WithRequired(e => e.Phong)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Phong>()
                .HasMany(e => e.SinhViens)
                .WithRequired(e => e.Phong)
                .HasForeignKey(e => e.MaPhong)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QuanLy>()
                .Property(e => e.MaQL)
                .IsUnicode(false);

            modelBuilder.Entity<QuanLy>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<QuanLy>()
                .Property(e => e.CMND)
                .IsUnicode(false);

            modelBuilder.Entity<QuanLy>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<QuanLy>()
                .HasMany(e => e.DonDangKies)
                .WithRequired(e => e.QuanLy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QuanLy>()
                .HasMany(e => e.HoaDonDiens)
                .WithRequired(e => e.QuanLy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QuanLy>()
                .HasMany(e => e.HoaDonTienPhongs)
                .WithRequired(e => e.QuanLy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QuanLy>()
                .HasMany(e => e.Phongs)
                .WithRequired(e => e.QuanLy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.MSSV)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.CMND)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.DiaChi)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.MaPhong)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .HasMany(e => e.DonDangKies)
                .WithRequired(e => e.SinhVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SinhVien>()
                .HasMany(e => e.HoaDonTienPhongs)
                .WithRequired(e => e.SinhVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SinhVien>()
                .HasMany(e => e.Phongs)
                .WithOptional(e => e.SinhVien)
                .HasForeignKey(e => e.MaTruongPhong);
        }
    }
}
