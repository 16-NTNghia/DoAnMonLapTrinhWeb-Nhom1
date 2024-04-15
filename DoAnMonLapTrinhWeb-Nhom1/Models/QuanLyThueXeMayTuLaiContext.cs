using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DoAnMonLapTrinhWeb_Nhom1.Models;

public partial class QuanLyThueXeMayTuLaiContext : DbContext
{
    public QuanLyThueXeMayTuLaiContext()
    {
    }

    public QuanLyThueXeMayTuLaiContext(DbContextOptions<QuanLyThueXeMayTuLaiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChucVu> ChucVus { get; set; }

    public virtual DbSet<DanhGium> DanhGia { get; set; }

    public virtual DbSet<DiaDiem> DiaDiems { get; set; }

    public virtual DbSet<HangXe> HangXes { get; set; }


    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<LoaiXe> LoaiXes { get; set; }

    public virtual DbSet<MailTuDong> MailTuDongs { get; set; }

    public virtual DbSet<PhuongThucThanhToan> PhuongThucThanhToans { get; set; }

    public virtual DbSet<Quyen> Quyens { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<Xe> Xes { get; set; }

    public virtual DbSet<YeuCauDatXe> YeuCauDatXes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MSI\\SQLEXPRESS;Database=QuanLyThueXeMayTuLai;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChucVu>(entity =>
        {
            entity.HasKey(e => e.IdchucVu).HasName("PK__ChucVu__70FCCF65DD36D787");

            entity.ToTable("ChucVu");

            entity.Property(e => e.IdchucVu).HasColumnName("IDChucVu");
            entity.Property(e => e.TenChucVu).HasMaxLength(100);

            entity.HasMany(d => d.Idquyens).WithMany(p => p.IdchucVus)
                .UsingEntity<Dictionary<string, object>>(
                    "PhanQuyen",
                    r => r.HasOne<Quyen>().WithMany()
                        .HasForeignKey("Idquyen")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PhanQuyen__IDQuy__787EE5A0"),
                    l => l.HasOne<ChucVu>().WithMany()
                        .HasForeignKey("IdchucVu")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PhanQuyen__IDChu__778AC167"),
                    j =>
                    {
                        j.HasKey("IdchucVu", "Idquyen").HasName("PK__PhanQuye__8BC6E742D5E035C7");
                        j.ToTable("PhanQuyen");
                        j.IndexerProperty<int>("IdchucVu").HasColumnName("IDChucVu");
                        j.IndexerProperty<int>("Idquyen").HasColumnName("IDQuyen");
                    });
        });

        modelBuilder.Entity<DanhGium>(entity =>
        {
            entity.HasKey(e => new { e.BienSoXe, e.Email }).HasName("PK__DanhGia__ED1D49C0256A5A2A");

            entity.Property(e => e.BienSoXe)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NhanXet).HasMaxLength(500);

            entity.HasOne(d => d.BienSoXeNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.BienSoXe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DanhGia__BienSoX__70DDC3D8");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DanhGia__Email__6E01572D");
        });

        modelBuilder.Entity<DiaDiem>(entity =>
        {
            entity.HasKey(e => e.MaDiaDiem).HasName("PK__DiaDiem__F015962A8780C2E6");

            entity.ToTable("DiaDiem");

            entity.Property(e => e.TenDiaDiem).HasMaxLength(20);
        });

        modelBuilder.Entity<HangXe>(entity =>
        {
            entity.HasKey(e => e.MaHangXe).HasName("PK__HangXe__8C6DD0A7F001ABFF");

            entity.ToTable("HangXe");

            entity.Property(e => e.TenHang).HasMaxLength(50);
        });



        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon).HasName("PK__HoaDon__835ED13B8B789FF1");

            entity.ToTable("HoaDon");

            entity.Property(e => e.BienSoXe)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NgayLap).HasColumnType("datetime");
            entity.Property(e => e.TongDonGia).HasColumnType("money");

            entity.HasOne(d => d.MaPhuongThucNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaPhuongThuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDon__MaPhuong__73BA3083");

            entity.HasOne(d => d.YeuCauDatXe).WithMany(p => p.HoaDons)
                .HasForeignKey(d => new { d.Email, d.BienSoXe })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDon__75A278F5");
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaiKhuyenMai).HasName("PK__KhuyenMa__BD611B46F103F3FB");

            entity.ToTable("KhuyenMai");

            entity.Property(e => e.NgayKhuyenMai).HasColumnType("datetime");
            entity.Property(e => e.NoiDungKhuyenMai).HasMaxLength(100);
            entity.Property(e => e.TenKhuyenMai)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LoaiXe>(entity =>
        {
            entity.HasKey(e => e.MaLoai).HasName("PK__LoaiXe__730A575939795964");

            entity.ToTable("LoaiXe");

            entity.Property(e => e.TenLoai).HasMaxLength(20);
        });

        modelBuilder.Entity<MailTuDong>(entity =>
        {
            entity.HasKey(e => e.MaMail).HasName("PK__MailTuDo__0DB26639A62061F8");

            entity.ToTable("MailTuDong");

            entity.Property(e => e.NoiDung).HasMaxLength(500);
            entity.Property(e => e.TieuDe).HasMaxLength(30);
        });

        modelBuilder.Entity<PhuongThucThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaPhuongThuc).HasName("PK__PhuongTh__35F7404EFCC16D3F");

            entity.ToTable("PhuongThucThanhToan");

            entity.Property(e => e.TenPhuongThuc).HasMaxLength(20);
        });

        modelBuilder.Entity<Quyen>(entity =>
        {
            entity.HasKey(e => e.Idquyen).HasName("PK__Quyen__B3A2827E136F0562");

            entity.ToTable("Quyen");

            entity.Property(e => e.Idquyen).HasColumnName("IDQuyen");
            entity.Property(e => e.TenQuyen).HasMaxLength(100);
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__TaiKhoan__A9D1053552AD3104");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Avarta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.Gplx)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("GPLX");
            entity.Property(e => e.IdchucVu).HasColumnName("IDChucVu");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.NgaySinh).HasColumnType("datetime");
            entity.Property(e => e.Sdt)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.TenNguoiDung).HasMaxLength(100);

            entity.HasOne(d => d.IdchucVuNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.IdchucVu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaiKhoan__IDChuc__76969D2E");
        });

        modelBuilder.Entity<Xe>(entity =>
        {
            entity.HasKey(e => e.BienSoXe).HasName("PK__Xe__A7805993CF3E80A0");

            entity.ToTable("Xe");

            entity.Property(e => e.BienSoXe)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GiaThue).HasColumnType("money");
            entity.Property(e => e.MaLucPhanKhoi)
                .HasMaxLength(10)
                .HasColumnName("MaLuc_PhanKhoi");
            entity.Property(e => e.MoTa).HasMaxLength(100);
            entity.Property(e => e.NamSanXuat)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.NhienLieu).HasMaxLength(10);
            entity.Property(e => e.TenXe)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Xes)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Xe__Email__6D0D32F4");

            entity.HasOne(d => d.MaDiaDiemNavigation).WithMany(p => p.Xes)
                .HasForeignKey(d => d.MaDiaDiem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Xe__MaDiaDiem__72C60C4A");

            entity.HasOne(d => d.MaHangXeNavigation).WithMany(p => p.Xes)
                .HasForeignKey(d => d.MaHangXe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Xe__MaHangXe__74AE54BC");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.Xes)
                .HasForeignKey(d => d.MaLoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Xe__MaLoai__71D1E811");
        });

        modelBuilder.Entity<YeuCauDatXe>(entity =>
        {
            entity.HasKey(e => new { e.Email, e.BienSoXe }).HasName("PK__YeuCauDa__83A900AC0247BA4F");

            entity.ToTable("YeuCauDatXe");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BienSoXe)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NgayNhan).HasColumnType("datetime");
            entity.Property(e => e.NgayTra).HasColumnType("datetime");

            entity.HasOne(d => d.BienSoXeNavigation).WithMany(p => p.YeuCauDatXes)
                .HasForeignKey(d => d.BienSoXe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__YeuCauDat__BienS__6FE99F9F");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.YeuCauDatXes)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__YeuCauDat__Email__6C190EBB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
