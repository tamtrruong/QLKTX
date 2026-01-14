using System;
using System.Collections.Generic;

namespace QLKTX_DAO.Models;

public partial class HoaDon
{
    public string MaHoaDon { get; set; } = null!;

    public string? MaPhong { get; set; }

    public DateOnly KyHoaDon { get; set; }

    public decimal TienPhong { get; set; }

    public decimal TienDien { get; set; }

    public decimal TienNuoc { get; set; }

    public decimal TienPhat { get; set; }

    public decimal? TongTien { get; set; }

    public byte TrangThai { get; set; }
    public DateTime? NgayLap { get; set; }
    public DateTime? NgayThanhToan { get; set; }

    public byte? PhuongThucTt { get; set; }

    public virtual Phong? MaPhongNavigation { get; set; }
}
