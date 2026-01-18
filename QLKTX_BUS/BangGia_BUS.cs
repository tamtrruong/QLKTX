using QLKTX_DAO;
using QLKTX_DAO.Models;
using QLKTX_DTO;

namespace QLKTX_BUS
{
    public class BangGia_BUS
    {
        private readonly BangGia_DAO dao;

        public BangGia_BUS(BangGia_DAO dao)
        {
            this.dao = dao;
        }

        public async Task<List<BangGia_DTO>> GetAllAsync()
        {
            var data = await dao.GetAllAsync();
            return data.Select(x => new BangGia_DTO
            {
                MaBangGia = x.MaBangGia,
                LoaiPhong = x.LoaiPhong,
                DonGiaPhong = x.DonGiaPhong,
                DonGiaDien = x.DonGiaDien,
                DonGiaNuoc = x.DonGiaNuoc,
                PhiRac = x.PhiRac,
                DangSuDung = x.DangSuDung
            }).ToList();
        }
            
        public async Task AddAsync(BangGia_DTO dto)
        {
            var current = await dao.GetDangSuDungAsync();
            if (current != null)
            {
                current.DangSuDung = false;
                await dao.UpdateAsync(current);
            }

            var entity = new BangGium
            {
                LoaiPhong = dto.LoaiPhong,
                DonGiaPhong = dto.DonGiaPhong,
                DonGiaDien = dto.DonGiaDien,
                DonGiaNuoc = dto.DonGiaNuoc,
                PhiRac = dto.PhiRac,
                DangSuDung = true
            };

            await dao.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(BangGia_DTO dto)
        {
            var entity = await dao.GetByIdAsync(dto.MaBangGia);
            if (entity == null)
                return false;

            // Nếu set bảng giá này là đang sử dụng
            if (dto.DangSuDung == true)
            {
                var current = await dao.GetDangSuDungAsync();
                if (current != null && current.MaBangGia != dto.MaBangGia)
                {
                    current.DangSuDung = false;
                    await dao.UpdateAsync(current);
                }
            }

            entity.LoaiPhong = dto.LoaiPhong;
            entity.DonGiaPhong = dto.DonGiaPhong;
            entity.DonGiaDien = dto.DonGiaDien;
            entity.DonGiaNuoc = dto.DonGiaNuoc;
            entity.PhiRac = dto.PhiRac;
            entity.DangSuDung = dto.DangSuDung;

            await dao.UpdateAsync(entity);
            return true;
        }
    }
}
