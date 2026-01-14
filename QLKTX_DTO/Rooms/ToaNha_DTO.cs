namespace QLKTX_DTO.Rooms
{
    public class ToaNha_DTO
    {
        public string MaToa { get; set; }
        public string TenToa { get; set; }
        public byte LoaiToa { get; set; } 
        public string TenLoaiToa => LoaiToa == 0 ? "Khu Nam" : "Khu Nữ";
        public int SoLuongPhong { get; set; }
    }
}
