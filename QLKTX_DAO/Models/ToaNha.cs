using System;
using System.Collections.Generic;

namespace QLKTX_DAO.Models;

public partial class ToaNha
{
    public string MaToa { get; set; } = null!;

    public string TenToa { get; set; } = null!;

    public byte LoaiToa { get; set; }

    public virtual ICollection<Phong> Phongs { get; set; } = new List<Phong>();
}
