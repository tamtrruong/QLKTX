using QLKTX_DAO;
using QLKTX_DTO.Tke;

namespace QLKTX_BUS
{
    public class Thongke_BUS
    {
        private readonly ThongKe_DAO dao;

        public Thongke_BUS(ThongKe_DAO dao)
        {
            this.dao = dao;
        }

        public async Task<decimal> GetTongDoanhThuNamAsync(int nam)
        {
            var list = await dao.GetDoanhThuNam(nam);
            return list.Sum(x => x.TongDoanhThu);
        }

        public async Task<List<TK_TrangThaiHD_DTO>> GetTyLeThanhToanAsync()
        {
            return await dao.GetThongKeTrangThaiHoaDon();
        }

        public async Task<List<DoanhThuPhong_DTO>> GetDoanhThuTheoPhongAsync(DateTime tuNgay, DateTime denNgay)
        {
            if (tuNgay > denNgay)
                throw new ArgumentException("Từ ngày phải nhỏ hơn đến ngày");

            return await dao.GetDoanhThuTheoPhong(tuNgay, denNgay);
        }
    }
}
