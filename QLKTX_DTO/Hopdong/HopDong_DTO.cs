namespace QLKTX_DTO.Hopdong
{
    public enum TinhTrangHopDong : byte
    {
        DangHieuLuc = 0,
        DaKetThuc = 1,
        BiChamDut = 2,
        ChoDuyet = 3
    }

    public class HopDong_DTO
    {
        public string MaHopDong { get; set; }
        public string MaSV { get; set; }
        public string HoTenSV { get; set; } 
        public string MaPhong { get; set; }
        public string TenPhong { get; set; } 
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int SoThang { get; set; }
        public TinhTrangHopDong TinhTrang { get; set; }

        public string TenTinhTrang => TinhTrang switch
        {
            TinhTrangHopDong.DangHieuLuc => "Đang hiệu lực",
            TinhTrangHopDong.DaKetThuc => "Đã kết thúc",
            TinhTrangHopDong.BiChamDut => "Bị chấm dứt (Vi phạm)",
            _ => "Chờ duyệt"
        };
    }
}
