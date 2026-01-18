
using QLKTX_DTO.Bill;

namespace QLKTX_DTO.Tke
{
    public class TK_TrangThaiHD_DTO
    {
        public TrangThaiHD TrangThai { get; set; }

        public string TenTrangThai => TrangThai switch
        {
            TrangThaiHD.DaThanhToan => "Đã thanh toán",
            TrangThaiHD.ChuaThanhToan => "Chưa thanh toán",
            _ => "Không xác định"
        };
        public int SoLuong { get; set; }
        public decimal TongTien { get; set; }
    }
}
