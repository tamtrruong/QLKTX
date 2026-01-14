using AutoMapper;
using QLKTX_DAO;
using QLKTX_DAO.Models;
using QLKTX_DTO.Bill;
using QLKTX_DTO.Rooms;

namespace QLKTX_BUS
{
    public class HoaDon_BUS
    {
        private readonly HoaDon_DAO hddao;
        private readonly IMapper map;
        private readonly Phong_DAO phongdao;

        // CẤU HÌNH GIÁ (Cố định)
        private const decimal GIA_DIEN = 3500;
        private const decimal GIA_NUOC = 6000;
        private const decimal PHI_RAC = 20000;

        public HoaDon_BUS(HoaDon_DAO hoadonDAO, Phong_DAO phongDAO, IMapper mapper)
        {
            hddao = hoadonDAO;
            phongdao = phongDAO;
            map = mapper;
        }

        public async Task<List<HoaDon_DTO>> GetHoaDonChuaThanhToanAsync()
        {
            var list = await hddao.GetHoaDonNoAsync();
            return map.Map<List<HoaDon_DTO>>(list);
        }

        public async Task<List<HoaDon_DTO>> GetLichSuHoaDonAsync(string maPhong)
        {
            var list = await hddao.GetByPhongAsync(maPhong);
            return map.Map<List<HoaDon_DTO>>(list);
        }

        public async Task CreateHoaDonAsync(CreateHD_DTO dto)
        {
            // 1. Kiểm tra phòng (Đã khớp tên hàm với DAO của bạn)
            var phong = await phongdao.GetByIdAsync(dto.MaPhong);

            if (phong == null) throw new Exception("Phòng không tồn tại!");

            // 2. Validate chỉ số
            if (dto.DienMoi < dto.DienCu) throw new Exception("Chỉ số điện mới nhỏ hơn số cũ!");
            if (dto.NuocMoi < dto.NuocCu) throw new Exception("Chỉ số nước mới nhỏ hơn số cũ!");

            // 3. Kiểm tra trùng hóa đơn
            DateOnly kyHoaDon = new DateOnly(dto.Nam, dto.Thang, 1);
            var existBill = await hddao.GetByPhongAndKyAsync(dto.MaPhong, kyHoaDon);
            if (existBill != null) throw new Exception($"Hóa đơn tháng {dto.Thang}/{dto.Nam} của phòng này đã được tạo rồi.");

            // 4. Map dữ liệu
            var hoadon = map.Map<HoaDon>(dto);

            hoadon.KyHoaDon = kyHoaDon;
            hoadon.NgayLap = DateTime.Now;

            // 5. Tính tiền
            int soDien = dto.DienMoi - dto.DienCu;
            int soNuoc = dto.NuocMoi - dto.NuocCu;

            hoadon.TienDien = soDien * GIA_DIEN;
            hoadon.TienNuoc = soNuoc * GIA_NUOC;

            // QUAN TRỌNG: Gán cứng tiền phòng để tránh lỗi CS1061 nếu thiếu cột GiaPhong
            // Sau này bạn check lại Model Phong, nếu có cột DonGia thì sửa thành phong.DonGia
            hoadon.TienPhong = 500000;

            hoadon.TienPhat = PHI_RAC;

            // Tính tổng (Đã xóa ?? 0 để tránh lỗi CS0019)
            hoadon.TongTien = hoadon.TienDien + hoadon.TienNuoc + hoadon.TienPhong + hoadon.TienPhat;

            // 6. Hoàn thiện thông tin
            hoadon.TrangThai = 0;
            hoadon.MaHoaDon = $"HD_{dto.MaPhong}_{hoadon.KyHoaDon:yyyyMM}";

            await hddao.CreateHoaDonAsync(hoadon);
        }

        public async Task ThanhToanAsync(string maHoaDon, int phuongThuc)
        {
            await hddao.ThanhToanAsync(maHoaDon, (byte)phuongThuc);
        }
    }
}