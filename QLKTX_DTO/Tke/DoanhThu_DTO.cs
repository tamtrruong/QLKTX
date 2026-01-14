
namespace QLKTX_DTO.Tke
{
    public class DoanhThu_DTO
    {
        public int Thang { get; set; }
        public int Nam { get; set; }
        public decimal TongDoanhThu { get; set; }
        public decimal DaThu { get; set; }
        public decimal ConNo => TongDoanhThu - DaThu;
    }
}
