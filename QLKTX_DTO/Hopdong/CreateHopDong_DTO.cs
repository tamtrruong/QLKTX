using System.ComponentModel.DataAnnotations;
namespace QLKTX_DTO.Hopdong
{
    public class CreateHopDong_DTO
    {
        public string MaSV { get; set; }
        public string MaPhong { get; set; }
        public DateTime NgayBatDau { get; set; }
        public int SoThang { get; set; } 
    }
}
