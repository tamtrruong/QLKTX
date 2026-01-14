using AutoMapper;
using QLKTX_DAO;
using QLKTX_DAO.Models;
using QLKTX_DTO.Hopdong;

namespace QLKTX_BUS
{
    public class HopDong_BUS
    {
        private readonly HopDong_DAO hddao;
        private readonly IMapper map;

        public HopDong_BUS(HopDong_DAO hopDongDAO, IMapper mapper)
        {
            hddao = hopDongDAO;
            map = mapper;
        }

        public async Task CreateHopDongAsync(CreateHopDong_DTO dto)
        {
            bool isHasRoom = await hddao.IsSinhVienCoPhong(dto.MaSV);
            if (isHasRoom) throw new Exception("Sinh viên này đang có hợp đồng hiệu lực!");

            var entity = map.Map<HopDong>(dto);

            if (dto.SoThang > 0)
            {
                entity.NgayKetThuc = entity.NgayBatDau.AddMonths(dto.SoThang);
            }
            else
            {
                entity.NgayKetThuc = entity.NgayBatDau.AddMonths(12);
                entity.SoThang = 12;
            }

            
            entity.MaHopDong = $"HD_{DateTime.Now:yyyyMMddHHmmss}";
            entity.TinhTrang = 0; 
            
            // SỬA: XÓA DÒNG entity.NgayLap = DateTime.Now; ĐI
            // (Vì bảng HopDong của bạn không có cột này)

            await hddao.CreateHopDong(entity);
        }

        public async Task ThanhLyHopDongAsync(string maHD)
        {
            await hddao.ThanhLyHopDong(maHD);
        }

        public async Task<List<HopDong_DTO>> GetHopDongSapHetHanAsync()
        {
            var list = await hddao.GetHopDongSapHetHan();
            return map.Map<List<HopDong_DTO>>(list);
        }
        // Thêm vào bên dưới
        public async Task<List<HopDong_DTO>> GetAllHopDongsAsync()
        {
            var list = await hddao.GetAllHopDongs();
            return map.Map<List<HopDong_DTO>>(list);
        }
    }
}