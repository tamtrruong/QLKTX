using System;
using System.Collections.Generic;

namespace QLKTX_DAO.Models;

public partial class TaiKhoan
{
    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public byte Quyen { get; set; }

    public string? MaSv { get; set; }

    public virtual SinhVien? MaSvNavigation { get; set; }
}
