namespace QLKTX_DTO
{
    public class BangGia_DTO
    {
        public int MaBangGia { get; set; }
        public string LoaiPhong { get; set; }
        public decimal DonGiaPhong { get; set; }
        public decimal DonGiaDien { get; set; }
        public decimal DonGiaNuoc { get; set; }
        public decimal PhiRac { get; set; }
        public DateTime NgayApDung { get; set; }
        public bool? DangSuDung { get; set; }
    }
}
