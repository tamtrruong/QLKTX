using System;
using System.Collections.Generic;

namespace QLKTX_DAO.Models;

public partial class Phong
{
    public string MaPhong { get; set; } = null!;

    public string TenPhong { get; set; } = null!;

    public int? Tang { get; set; }

    public int SucChua { get; set; }

    public int SoNguoiHienTai { get; set; }

    public byte? TrangThai { get; set; }

    public string? MaToa { get; set; }

    public string? LoaiPhong { get; set; }

    public virtual ICollection<DienNuoc> DienNuocs { get; set; } = new List<DienNuoc>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual ICollection<HopDong> HopDongs { get; set; } = new List<HopDong>();

    public virtual ToaNha? MaToaNavigation { get; set; }
}
