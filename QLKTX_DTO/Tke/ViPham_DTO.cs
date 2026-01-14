namespace QLKTX_DTO.Tke
{
    public enum MucDoViPham : byte
    {
        Nhe = 0,
        TrungBinh = 1,
        Nang = 2
    }

    public class ViPham_DTO
    {
        public int MaViPham { get; set; }
        public string MaSV { get; set; }
        public string HoTenSV { get; set; }
        public string NoiDung { get; set; }
        public MucDoViPham? MucDo { get; set; }
        public string TenMucDo => MucDo switch
        {
            MucDoViPham.Nhe => "Nhẹ (Nhắc nhở)",
            MucDoViPham.TrungBinh => "Trung bình (Cảnh cáo)",
            MucDoViPham.Nang => "Nặng (Thanh lý HĐ)",
            _ => "Chưa xác định"
        };
        public string HinhThucXuLy { get; set; }
        public DateTime NgayViPham { get; set; }
        public string GhiChu { get; set; }
    }
}
