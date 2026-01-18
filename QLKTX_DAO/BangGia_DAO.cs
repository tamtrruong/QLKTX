using Microsoft.EntityFrameworkCore;
using QLKTX_DAO.Models;

namespace QLKTX_DAO
{
    public class BangGia_DAO
    {
        private readonly QLKTXContext context;

        public BangGia_DAO(QLKTXContext context)
        {
            this.context = context;
        }

        public async Task<List<BangGium>> GetAllAsync()
        {
            return await context.BangGia.ToListAsync();
        }

        public async Task<BangGium?> GetDangSuDungAsync()
        {
            return await context.BangGia
                .FirstOrDefaultAsync(x => x.DangSuDung == true);
        }

        public async Task AddAsync(BangGium entity)
        {
            context.BangGia.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BangGium entity)
        {
            context.BangGia.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task<BangGium?> GetByIdAsync(int id)
        {
            return await context.BangGia.FindAsync(id);
        }

    }
}
