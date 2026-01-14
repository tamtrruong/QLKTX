using System.ComponentModel.DataAnnotations;


namespace QLKTX_DTO.Auth
{
    public class Register_DTO
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải dài hơn 6 ký tự")]
        public string MatKhau { get; set; }

        [Compare("MatKhau", ErrorMessage = "Mật khẩu nhập lại không khớp")]
        public string NhapLaiMatKhau { get; set; }

        // Các thông tin phụ để tạo user
        public string? HoTen { get; set; }
        public string? MaSV { get; set; }
        public string? Email { get; set; }
        public QuyenNguoiDung VaiTro { get; set; } = QuyenNguoiDung.SinhVien;
    }
}
