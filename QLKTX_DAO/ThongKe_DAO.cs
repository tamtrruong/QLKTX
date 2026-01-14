using QLKTX_DAO.Models;
using Microsoft.EntityFrameworkCore;
using QLKTX_DTO.Tke;
using QLKTX_DTO.Bill;

namespace QLKTX_DAO
{
    public class ThongKe_DAO
    {
        private readonly QLKTXContext _context;

        public ThongKe_DAO(QLKTXContext context)
        {
            _context = context;
        }

        public async Task<List<DoanhThu_DTO>> GetDoanhThuNam(int nam)
        {
            var result = await _context.Database
                .SqlQuery<DoanhThu_DTO>($"EXEC sp_ThongKeDoanhThuTheoNam @Nam = {nam}")
                .ToListAsync();

            return result;
        }

        public async Task<List<HoaDon_DTO>> GetThongKeTrangThaiHoaDon()
        {
            FormattableString sql = $"EXEC sp_ThongKeHoaDonTheoTrangThai";

            return await _context.Database
                .SqlQuery<HoaDon_DTO>(sql)
                .ToListAsync();
        }
    }
}
