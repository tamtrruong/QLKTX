using System;
using System.Collections.Generic;

namespace QLKTX_DAO.Models;

public partial class BangGium
{
    public int MaBangGia { get; set; }

    public string LoaiPhong { get; set; } = null!;

    public decimal DonGiaPhong { get; set; }

    public decimal DonGiaDien { get; set; }

    public decimal DonGiaNuoc { get; set; }

    public decimal PhiRac { get; set; }

    public DateTime? NgayApDung { get; set; }

    public bool? DangSuDung { get; set; }
}
