using QLKTX_DAO.Models;
using Microsoft.EntityFrameworkCore;

namespace QLKTX_DAO
{
    public class Phong_DAO
    {
        private readonly QLKTXContext _context;

        public Phong_DAO(QLKTXContext context)
        {
            _context = context;
        }

        public async Task<List<Phong>> GetAllPhongWithToaNha()
        {
            return await _context.Phongs
                .Include(p => p.MaToaNavigation) 
                .AsNoTracking()
                .ToListAsync();
        }

        // Tìm phòng trống (SoNguoiHienTai < SucChua)
        public async Task<List<Phong>> GetPhongTrongAsync()
        {
            return await _context.Phongs
                .Where(p => p.SoNguoiHienTai < p.SucChua && p.TrangThai == 0) 
                .Include(p => p.MaToaNavigation)
                .ToListAsync();
        }

        public async Task<Phong?> GetByIdAsync(string maPhong)
        {
            return await _context.Phongs.FindAsync(maPhong);
        }

        // Lấy danh sách phòng theo Tòa
        public async Task<List<Phong>> GetByMaToaAsync(string maToa)
        {
            return await _context.Phongs
                .Where(p => p.MaToa == maToa)
                .ToListAsync();
        }
    }
}
