using Microsoft.EntityFrameworkCore;
using QLKTX_DAO.Models;

namespace QLKTX_DAO
{
    public class DienNuoc_DAO
    {
        private readonly QLKTXContext _context;

        public DienNuoc_DAO(QLKTXContext context)
        {
            _context = context;
        }

        public async Task AddChiSo(DienNuoc dn)
        {
            if (dn.DienMoi < dn.DienCu || dn.NuocMoi < dn.NuocCu)
            {
                throw new Exception("Chỉ số mới không được nhỏ hơn chỉ số cũ.");
            }
            bool exists = await _context.DienNuocs
                .AnyAsync(x => x.MaPhong == dn.MaPhong && x.KyGhiNhan == dn.KyGhiNhan);

            if (exists) throw new Exception("Tháng này đã ghi điện nước rồi.");

            _context.DienNuocs.Add(dn);
            await _context.SaveChangesAsync();
        }

        public async Task<DienNuoc?> GetLastChiSo(string maPhong)
        {
            return await _context.DienNuocs
                .Where(dn => dn.MaPhong == maPhong)
                .OrderByDescending(dn => dn.KyGhiNhan)
                .FirstOrDefaultAsync();
        }
    }
}
