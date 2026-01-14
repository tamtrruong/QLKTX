using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLKTX_DAO.Models;

public partial class QLKTXContext : DbContext
{
    public QLKTXContext()
    {
    }

    public QLKTXContext(DbContextOptions<QLKTXContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BangGium> BangGia { get; set; }

    public virtual DbSet<DienNuoc> DienNuocs { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<HopDong> HopDongs { get; set; }

    public virtual DbSet<Phong> Phongs { get; set; }

    public virtual DbSet<SinhVien> SinhViens { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<ToaNha> ToaNhas { get; set; }

    public virtual DbSet<ViPham> ViPhams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=QLKTX;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BangGium>(entity =>
        {
            entity.HasKey(e => e.MaBangGia).HasName("PK__BangGia__5DAC5A688A62B9AD");

            entity.Property(e => e.DangSuDung).HasDefaultValue(true);
            entity.Property(e => e.DonGiaDien).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.DonGiaNuoc).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.DonGiaPhong).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.LoaiPhong).HasMaxLength(50);
            entity.Property(e => e.NgayApDung)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PhiRac).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<DienNuoc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DienNuoc__3214EC27F52263FD");

            entity.ToTable("DienNuoc");

            entity.HasIndex(e => new { e.MaPhong, e.KyGhiNhan }, "UQ_DienNuoc").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MaPhong)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NgayChot)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaPhongNavigation).WithMany(p => p.DienNuocs)
                .HasForeignKey(d => d.MaPhong)
                .HasConstraintName("FK__DienNuoc__MaPhon__5535A963");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon).HasName("PK__HoaDon__835ED13B80E191B6");

            entity.ToTable("HoaDon");

            entity.HasIndex(e => new { e.MaPhong, e.KyHoaDon }, "UQ_HoaDon").IsUnique();

            entity.Property(e => e.MaHoaDon)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MaPhong)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NgayThanhToan).HasColumnType("datetime");
            entity.Property(e => e.PhuongThucTt).HasColumnName("PhuongThucTT");
            entity.Property(e => e.TienDien).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TienNuoc).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TienPhat).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TienPhong).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TongTien)
                .HasComputedColumnSql("((([TienPhong]+[TienDien])+[TienNuoc])+[TienPhat])", true)
                .HasColumnType("decimal(21, 0)");

            entity.HasOne(d => d.MaPhongNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaPhong)
                .HasConstraintName("FK__HoaDon__MaPhong__619B8048");
        });

        modelBuilder.Entity<HopDong>(entity =>
        {
            entity.HasKey(e => e.MaHopDong).HasName("PK__HopDong__36DD43429C347577");

            entity.ToTable("HopDong", tb =>
                {
                    tb.HasTrigger("trg_GiamSiSoPhong");
                    tb.HasTrigger("trg_TangSiSoPhong");
                });

            entity.HasIndex(e => new { e.MaSv, e.MaPhong, e.NgayBatDau }, "UQ_HopDong").IsUnique();

            entity.Property(e => e.MaHopDong)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MaPhong)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaSv)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MaSV");
            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");
            entity.Property(e => e.TinhTrang).HasDefaultValue((byte)0);

            entity.HasOne(d => d.MaPhongNavigation).WithMany(p => p.HopDongs)
                .HasForeignKey(d => d.MaPhong)
                .HasConstraintName("FK__HopDong__MaPhong__4F7CD00D");

            entity.HasOne(d => d.MaSvNavigation).WithMany(p => p.HopDongs)
                .HasForeignKey(d => d.MaSv)
                .HasConstraintName("FK__HopDong__MaSV__4E88ABD4");
        });

        modelBuilder.Entity<Phong>(entity =>
        {
            entity.HasKey(e => e.MaPhong).HasName("PK__Phong__20BD5E5B6263C500");

            entity.ToTable("Phong");

            entity.Property(e => e.MaPhong)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LoaiPhong).HasMaxLength(50);
            entity.Property(e => e.MaToa)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenPhong).HasMaxLength(50);
            entity.Property(e => e.TrangThai).HasDefaultValue((byte)0);

            entity.HasOne(d => d.MaToaNavigation).WithMany(p => p.Phongs)
                .HasForeignKey(d => d.MaToa)
                .HasConstraintName("FK__Phong__MaToa__45F365D3");
        });

        modelBuilder.Entity<SinhVien>(entity =>
        {
            entity.HasKey(e => e.MaSv).HasName("PK__SinhVien__2725081AFA13D9FE");

            entity.ToTable("SinhVien");

            entity.Property(e => e.MaSv)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MaSV");
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.Lop).HasMaxLength(50);
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SDT");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.TenDangNhap).HasName("PK__TaiKhoan__55F68FC18C8A5767");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.TenDangNhap)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MaSv)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MaSV");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.MaSvNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaSv)
                .HasConstraintName("FK__TaiKhoan__MaSV__4AB81AF0");
        });

        modelBuilder.Entity<ToaNha>(entity =>
        {
            entity.HasKey(e => e.MaToa).HasName("PK__ToaNha__31493444A469B9A7");

            entity.ToTable("ToaNha");

            entity.Property(e => e.MaToa)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenToa).HasMaxLength(50);
        });

        modelBuilder.Entity<ViPham>(entity =>
        {
            entity.HasKey(e => e.MaViPham).HasName("PK__ViPham__F1921D892DE27050");

            entity.ToTable("ViPham");

            entity.Property(e => e.HinhThucXuLy).HasMaxLength(50);
            entity.Property(e => e.MaSv)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MaSV");
            entity.Property(e => e.NgayViPham)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NoiDung).HasMaxLength(500);

            entity.HasOne(d => d.MaSvNavigation).WithMany(p => p.ViPhams)
                .HasForeignKey(d => d.MaSv)
                .HasConstraintName("FK__ViPham__MaSV__6A30C649");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
