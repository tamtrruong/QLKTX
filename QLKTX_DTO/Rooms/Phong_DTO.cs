
namespace QLKTX_DTO.Rooms
{
    public enum TrangThaiPhong : byte
    {
        ConTrong = 0,
        DaDay = 1,
        BaoTri = 2
    }

    public class Phong_DTO
    {
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public int Tang { get; set; }
        public int SucChua { get; set; }
        public int SoNguoiHienTai { get; set; }
        public TrangThaiPhong TrangThai { get; set; }
        public string TenTrangThai => TrangThai switch
        {
            TrangThaiPhong.ConTrong => "Còn trống",
            TrangThaiPhong.DaDay => "Đã đầy",
            TrangThaiPhong.BaoTri => "Đang bảo trì",
            _ => "Không xác định"
        };
        public string MaToa { get; set; }
        public string TenToa { get; set; }
        public string LoaiPhong { get; set; }
        public decimal DonGiaPhongHienTai { get; set; }
    }
}
