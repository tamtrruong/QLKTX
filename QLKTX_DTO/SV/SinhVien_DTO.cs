namespace QLKTX_DTO.SV
{
    public enum Genders : byte
    {
        Nam = 0,
        Nu = 1
    }
    public class SinhVien_DTO
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public Genders? GioiTinh { get; set; }

        public string TenGioiTinh => GioiTinh switch
        {
            Genders.Nam => "Nam",
            Genders.Nu => "Nữ",
            _ => "Chưa xác định"
        };
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public string Lop { get; set; }
        public string MaPhongHienTai { get; set; }
    }
}
