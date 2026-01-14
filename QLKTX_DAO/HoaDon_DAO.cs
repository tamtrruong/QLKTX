using QLKTX_DAO.Models;
using Microsoft.EntityFrameworkCore;

namespace QLKTX_DAO
{
    public class HoaDon_DAO
    {
        private readonly QLKTXContext context;

        public HoaDon_DAO(QLKTXContext context)
        {
            this.context = context;
        }

        public async Task<List<HoaDon>> GetByPhongAsync(string maPhong)
        {
            return await context.HoaDons
                .Where(hd => hd.MaPhong == maPhong)
                .OrderByDescending(hd => hd.KyHoaDon) // Mới nhất lên đầu
                .ToListAsync();
        }

        public async Task<HoaDon?> GetByPhongAndKyAsync(string maPhong, DateOnly kyHoaDon)
        {
            return await context.HoaDons
                .FirstOrDefaultAsync(h => h.MaPhong == maPhong && h.KyHoaDon == kyHoaDon);
        }

        public async Task<List<HoaDon>> GetHoaDonNoAsync()
        {
            return await context.HoaDons
                .Where(hd => hd.TrangThai == 0)
                .Include(hd => hd.MaPhongNavigation) 
                .OrderBy(hd => hd.KyHoaDon) 
                .ToListAsync();
        }

        public async Task<List<HoaDon>> GetAllAsync()
        {
            return await context.HoaDons
                .Include(hd => hd.MaPhongNavigation)
                .OrderByDescending(hd => hd.KyHoaDon)
                .ToListAsync();
        }

        public async Task CreateHoaDonAsync(HoaDon hoadon)
        {
            bool exists = await context.HoaDons.AnyAsync(x => x.MaPhong == hoadon.MaPhong && x.KyHoaDon == hoadon.KyHoaDon);
            if (exists)
            {
                throw new Exception($"Phòng {hoadon.MaPhong} đã có hóa đơn cho kỳ này rồi.");
            }

            context.HoaDons.Add(hoadon);
            await context.SaveChangesAsync();
        }

        public async Task ThanhToanAsync(string maHoaDon, byte phuongThucTT)
        {
            var hd = await context.HoaDons.FindAsync(maHoaDon);
            if (hd == null) throw new Exception("Không tìm thấy hóa đơn.");
            if (hd.TrangThai == 1) throw new Exception("Hóa đơn này đã được thanh toán rồi.");
            hd.TrangThai = 1;
            hd.NgayThanhToan = DateTime.Now;
            hd.PhuongThucTt = phuongThucTT;

            context.HoaDons.Update(hd);
            await context.SaveChangesAsync();
        }
    }
}
