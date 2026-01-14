using QLKTX_DAO;
using QLKTX_DTO.Bill;
using QLKTX_DTO.Tke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKTX_BUS
{
    public class Thongke_BUS
    {
        private readonly ThongKe_DAO dao;

        public Thongke_BUS(ThongKe_DAO dao)
        {
            this.dao = dao;
        }

        public async Task<List<DoanhThu_DTO>> GetDoanhThuNamAsync(int nam)
        {
            return await dao.GetDoanhThuNam(nam);
        }

        public async Task<List<HoaDon_DTO>> GetTyLeThanhToanAsync()
        {
            return await dao.GetThongKeTrangThaiHoaDon();
        }
    }
}
