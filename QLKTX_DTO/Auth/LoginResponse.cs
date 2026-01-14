namespace QLKTX_DTO.Auth
{
    public enum QuyenNguoiDung : byte
    {
        Admin = 0,
        SinhVien = 1
    }

    public class LoginResponse
    {
        public string Token { get; set; } 
        public DateTime Expiration { get; set; } 
        public string TenDangNhap { get; set; }
        public string HoTen { get; set; }
        public string MaSV { get; set; }
        public QuyenNguoiDung Quyen { get; set; }
        public string TenQuyen => Quyen switch
        {
            QuyenNguoiDung.Admin => "Quản Trị Viên",
            QuyenNguoiDung.SinhVien => "Sinh Viên",
            _ => "Khác"
        };
    }
}
