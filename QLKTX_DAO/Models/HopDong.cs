using System;
using System.Collections.Generic;

namespace QLKTX_DAO.Models;

public partial class HopDong
{
    public string MaHopDong { get; set; } = null!;

    public string? MaSv { get; set; }

    public string? MaPhong { get; set; }

    public DateTime NgayBatDau { get; set; }

    public DateTime NgayKetThuc { get; set; }

    public int SoThang { get; set; }

    public byte? TinhTrang { get; set; }

    public virtual Phong? MaPhongNavigation { get; set; }

    public virtual SinhVien? MaSvNavigation { get; set; }
}
