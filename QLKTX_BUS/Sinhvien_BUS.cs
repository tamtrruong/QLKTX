using AutoMapper;
using QLKTX_DAO;
using QLKTX_DAO.Models;
using QLKTX_DTO.SV;

namespace QLKTX_BUS
{
    public class Sinhvien_BUS
    {
        private readonly Sinhvien_DAO dao;
        private readonly IMapper map;

        public Sinhvien_BUS(Sinhvien_DAO dao, IMapper mapper)
        {
            this.dao = dao;
            map = mapper;
        }

        public async Task<List<SinhVien_DTO>> GetAllAsync()
        {
            var listEntity = await dao.GetAllAsync();
            return map.Map<List<SinhVien_DTO>>(listEntity);
        }

        public async Task<SinhVien_DTO?> GetByIdAsync(string maSV)
        {
            var entity = await dao.GetByIdAsync(maSV);
            return map.Map<SinhVien_DTO>(entity);
        }

        public async Task CreateAsync(CreateSV_DTO dto)
        {
            // Kiểm tra trùng mã SV (nếu cần thiết, hoặc để DB tự báo lỗi)
            var entity = map.Map<SinhVien>(dto);
            // Ví dụ quy tắc: SV + ngày tháng năm + số ngẫu nhiên -> SV2024052099
            string prefix = "SV" + DateTime.Now.ToString("yyyyMMdd");
            Random rd = new Random();
            entity.MaSv = prefix + rd.Next(10, 99);
            await dao.AddAsync(entity);
        }

        public async Task UpdateAsync(SinhVien_DTO dto)
        {
            var entity = map.Map<SinhVien>(dto);
            await dao.UpdateAsync(entity);
        }

        public async Task DeleteAsync(string maSV)
        {
            await dao.DeleteAsync(maSV);
        }
    }
}
