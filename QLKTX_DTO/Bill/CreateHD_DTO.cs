using System.ComponentModel.DataAnnotations;

namespace QLKTX_DTO.Bill
{
    public class CreateHD_DTO
    {
        public string MaPhong { get; set; }

        [Range(1, 12, ErrorMessage = "Tháng phải từ 1 đến 12")]
        public int Thang { get; set; }

        [Range(2000, 2100, ErrorMessage = "Năm không hợp lệ")]
        public int Nam { get; set; }
        [Required]
        public int DienCu { get; set; }
        [Required]
        public int DienMoi { get; set; }
        [Required]
        public int NuocCu { get; set; }
        [Required]
        public int NuocMoi { get; set; }
    }
}
