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
            FormattableString sql = $"EXEC sp_ThongKeDoanhThuTheoNam @Nam = {nam}";
            return await _context.Database
                .SqlQuery<DoanhThu_DTO>(sql)
                .ToListAsync();
        }

        public async Task<List<TK_TrangThaiHD_DTO>> GetThongKeTrangThaiHoaDon()
        {
            FormattableString sql = $"EXEC sp_ThongKeHoaDonTheoTrangThai";

            return await _context.Database
                .SqlQuery<TK_TrangThaiHD_DTO>(sql)
                .ToListAsync();
        }

        public async Task<List<DoanhThuPhong_DTO>> GetDoanhThuTheoPhong(DateTime tuNgay, DateTime denNgay)
        {
            FormattableString sql =$"EXEC sp_ThongKeDoanhThuTheoPhong @TuNgay={tuNgay:yyyy-MM-dd}, @DenNgay={denNgay:yyyy-MM-dd}";

            return await _context.Database
                .SqlQuery<DoanhThuPhong_DTO>(sql)
                .ToListAsync();
        }
    }
}
