using QLKTX_DAO.Models;
using Microsoft.EntityFrameworkCore;

namespace QLKTX_DAO
{
    public class Sinhvien_DAO
    {
        private readonly QLKTXContext _context;

        public Sinhvien_DAO(QLKTXContext context)
        {
            _context = context;
        }

        public async Task<List<SinhVien>> GetAllAsync()
        {
            return await _context.SinhViens
                                 .AsNoTracking() // Tăng tốc độ nếu chỉ đọc dữ liệu
                                 .ToListAsync();
        }

        public async Task<SinhVien?> GetByIdAsync(string maSV)
        {
            return await _context.SinhViens.FindAsync(maSV);
        }

        public async Task AddAsync(SinhVien sv)
        {
            // Kiểm tra trùng lặp nếu cần
            if (await _context.SinhViens.AnyAsync(x => x.MaSv == sv.MaSv))
            {
                throw new Exception("Mã sinh viên đã tồn tại!");
            }

            _context.SinhViens.Add(sv);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SinhVien sv)
        {
            var existingSv = await _context.SinhViens.FindAsync(sv.MaSv);
            if (existingSv != null)
            {
                existingSv.HoTen = sv.HoTen;
                existingSv.NgaySinh = sv.NgaySinh;
                existingSv.GioiTinh = sv.GioiTinh;
                existingSv.Sdt = sv.Sdt;
                existingSv.DiaChi = sv.DiaChi;
                existingSv.Lop = sv.Lop;

                _context.SinhViens.Update(existingSv);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(string maSV)
        {
            var sv = await _context.SinhViens.FindAsync(maSV);
            if (sv != null)
            {
                _context.SinhViens.Remove(sv);
                await _context.SaveChangesAsync();
            }
        }
    }
}
