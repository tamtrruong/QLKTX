using QLKTX_DAO.Models;
using Microsoft.EntityFrameworkCore;
using QLKTX_DTO.Tke; // Để dùng Enum

namespace QLKTX_DAO
{
    public class ViPham_DAO
    {
        private readonly QLKTXContext _context;

        public ViPham_DAO(QLKTXContext context)
        {
            _context = context;
        }

        // 1. Thêm vi phạm mới
        public async Task CreateViPham(ViPham vp)
        {
            _context.ViPhams.Add(vp);
            await _context.SaveChangesAsync();
        }

        // 2. Đếm số lần vi phạm của sinh viên theo mức độ
        public async Task<int> CountViPhamByUser(string maSV, MucDoViPham mucDo)
        {
            // Đếm trong DB xem ông này đã bị bao nhiêu lỗi cùng mức độ này rồi
            return await _context.ViPhams
                .CountAsync(v => v.MaSv == maSV && v.MucDo == (byte)mucDo);
        }

        // 3. Lấy lịch sử vi phạm
        public async Task<List<ViPham>> GetHistory(string maSV)
        {
            var query = _context.ViPhams
                .Include(v => v.MaSvNavigation)
                .OrderByDescending(v => v.NgayViPham)
                .AsQueryable();

            // Nếu có mã SV thì lọc, không thì lấy hết (cho Admin xem tất cả)
            if (!string.IsNullOrEmpty(maSV))
            {
                query = query.Where(v => v.MaSv == maSV);
            }

            return await query.ToListAsync();
        }
    }
}