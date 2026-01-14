using System;

namespace QLKTX_DTO.Tke
{
    public class CreateViPham_DTO
    {
        public string MaSV { get; set; }
        public string NoiDung { get; set; }

        // Nhập 0 (Nhẹ), 1 (Trung bình), 2 (Nặng)
        public MucDoViPham MucDo { get; set; }

        public string HinhThucXuLy { get; set; } // Ví dụ: "Phạt 200k"
        public string GhiChu { get; set; }
        public DateTime NgayViPham { get; set; }
    }
}