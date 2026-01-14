using System;
using System.Collections.Generic;

namespace QLKTX_DAO.Models;

public partial class DienNuoc
{
    public int Id { get; set; }

    public string? MaPhong { get; set; }

    public DateOnly KyGhiNhan { get; set; }

    public int DienCu { get; set; }

    public int DienMoi { get; set; }

    public int NuocCu { get; set; }

    public int NuocMoi { get; set; }

    public DateTime? NgayChot { get; set; }

    public virtual Phong? MaPhongNavigation { get; set; }
}
