
using Microsoft.EntityFrameworkCore;
using QLKTX_DAO.Models;

namespace QLKTX_DAO
{
    public class TaiKhoan_DAO
    {
        private readonly QLKTXContext context;

        public TaiKhoan_DAO(QLKTXContext context)
        {
            this.context = context;
        }

        public async Task ChangePassword(string username, string oldPass, string newPass)
        {
            var user = await context.TaiKhoans.FindAsync(username);
            if (user == null) throw new Exception("Tài khoản không tồn tại");

            if (user.MatKhau != oldPass) throw new Exception("Mật khẩu cũ không đúng");

            user.MatKhau = newPass;
            context.TaiKhoans.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task<TaiKhoan?> GetByUsernameAsync(string username)
        {
            return await context.TaiKhoans.FirstOrDefaultAsync(u => u.TenDangNhap == username);
        }

        public async Task AddAccountAsync(TaiKhoan account)
        {
            context.TaiKhoans.Add(account);
            await context.SaveChangesAsync();
        }
    }
}
