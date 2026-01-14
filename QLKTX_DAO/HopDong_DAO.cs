using QLKTX_DAO.Models;
using Microsoft.EntityFrameworkCore;

namespace QLKTX_DAO
{
    public class HopDong_DAO
    {
        private readonly QLKTXContext _context;

        public HopDong_DAO(QLKTXContext context)
        {
            _context = context;
        }

        public async Task<bool> IsSinhVienCoPhong(string maSV)
        {
            return await _context.HopDongs
                .AnyAsync(hd => hd.MaSv == maSV && hd.TinhTrang == 0 && hd.NgayKetThuc > DateTime.Now);
        }

        public async Task CreateHopDong(HopDong hd)
        {
            if (await IsSinhVienCoPhong(hd.MaSv))
            {
                throw new Exception($"Sinh viên {hd.MaSv} đang có hợp đồng hiệu lực!");
            }

            var phong = await _context.Phongs.FindAsync(hd.MaPhong);
            if (phong == null) throw new Exception("Phòng không tồn tại.");

            // SỬA: Bỏ dấu ?? 0 vì SoNguoiHienTai là int (không null)
            int nguoiHienTai = phong.SoNguoiHienTai;

            if (nguoiHienTai >= phong.SucChua)
            {
                throw new Exception($"Phòng {hd.MaPhong} đã đủ người.");
            }

            _context.HopDongs.Add(hd);

            // Tăng số người
            phong.SoNguoiHienTai = nguoiHienTai + 1;

            await _context.SaveChangesAsync();
        }

        public async Task ThanhLyHopDong(string maHD)
        {
            var hd = await _context.HopDongs.FindAsync(maHD);

            if (hd != null && hd.TinhTrang == 0)
            {
                hd.TinhTrang = 1;
                hd.NgayKetThuc = DateTime.Now;

                var phong = await _context.Phongs.FindAsync(hd.MaPhong);
                // SỬA: Bỏ logic check null cho int, chỉ cần > 0 là được
                if (phong != null && phong.SoNguoiHienTai > 0)
                {
                    phong.SoNguoiHienTai -= 1;
                }

                _context.HopDongs.Update(hd);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<HopDong>> GetHopDongSapHetHan()
        {
            var today = DateTime.Now;
            var nextMonth = today.AddDays(30);

            return await _context.HopDongs
                .Where(h => h.TinhTrang == 0 && h.NgayKetThuc <= nextMonth && h.NgayKetThuc >= today)
                .Include(h => h.MaSvNavigation)
                .Include(h => h.MaPhongNavigation)
                .ToListAsync();
        }
   
        public async Task<List<HopDong>> GetAllHopDongs()
        {
            return await _context.HopDongs
                .Include(h => h.MaSvNavigation)
                .Include(h => h.MaPhongNavigation)
                .OrderByDescending(h => h.NgayBatDau)
                .ToListAsync();
        }
    }
}