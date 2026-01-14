using System;
using System.Collections.Generic;

namespace QLKTX_DAO.Models;

public partial class ViPham
{
    public int MaViPham { get; set; }

    public string? MaSv { get; set; }

    public string? NoiDung { get; set; }

    public byte? MucDo { get; set; }

    public DateTime? NgayViPham { get; set; }

    public string? HinhThucXuLy { get; set; }

    public string? GhiChu { get; set; }

    public virtual SinhVien? MaSvNavigation { get; set; }
}
