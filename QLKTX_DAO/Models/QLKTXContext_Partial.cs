using Microsoft.EntityFrameworkCore;
using QLKTX_DTO.Bill;
using QLKTX_DTO.Tke;

namespace QLKTX_DAO.Models
{
    /*
     Tại sao em tạo file Partial?
     ✔ Tránh mất cấu hình khi Scaffold lại
     ✔ Phân tách entity & DTO
     ✔ Dễ bảo trì
     */
    public partial class QLKTXContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoanhThu_DTO>().HasNoKey();
            modelBuilder.Entity<TK_TrangThaiHD_DTO>().HasNoKey();
            modelBuilder.Entity<DoanhThuPhong_DTO>().HasNoKey();
        }
    }
}
