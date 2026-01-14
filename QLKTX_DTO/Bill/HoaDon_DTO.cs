using QLKTX_DTO.Hopdong;

namespace QLKTX_DTO.Bill
{
    public enum TrangThaiHD : byte
    {
        DaThanhToan = 1,
        ChuaThanhToan = 0
    }

    public enum PaymentMethod : byte
    {
        TienMat = 0,
        ChuyenKhoan = 1,
        ViDienTu = 2
    }

    public class HoaDon_DTO
    {
        public string MaHoaDon { get; set; }
        public string MaPhong { get; set; }
        public string TenPhong { get; set; } 
        public DateOnly KyHoaDon { get; set; } 
        public decimal TienPhong { get; set; }
        public decimal TienDien { get; set; }
        public decimal TienNuoc { get; set; }
        public decimal TienPhat { get; set; }
        public decimal TongTien { get; set; }
        public TrangThaiHD TrangThai { get; set; }
        public string TenTrangThai => TrangThai switch
        {
            TrangThaiHD.DaThanhToan => "Đã thanh toán",
            TrangThaiHD.ChuaThanhToan => "Chưa thanh toán",
            _ => "Chờ duyệt"
        };
        public DateTime? NgayLap { get; set; }
        public DateTime? NgayThanhToan { get; set; }
        public PaymentMethod PhuongThucTT { get; set; }
        public string TenPhuongThucTT => PhuongThucTT switch
        {
            PaymentMethod.TienMat => "Tiền mặt",
            PaymentMethod.ChuyenKhoan => "Chuyển khoản",
            PaymentMethod.ViDienTu => "Ví điện tử",
            _ => "Khác"
        };
    }
}
