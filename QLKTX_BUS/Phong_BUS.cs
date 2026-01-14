using AutoMapper;
using QLKTX_DAO;
using QLKTX_DTO.Rooms;


namespace QLKTX_BUS
{
    public class Phong_BUS
    {
        private readonly Phong_DAO dao;
        private readonly IMapper map;

        public Phong_BUS(Phong_DAO dao, IMapper mapper)
        {
            this.dao = dao;
            map = mapper;
        }

        public async Task<List<Phong_DTO>> GetAllAsync()
        {
            var entities = await dao.GetAllPhongWithToaNha();
            return map.Map<List<Phong_DTO>>(entities);
        }

        public async Task<List<Phong_DTO>> GetPhongTrongAsync()
        {
            var entities = await dao.GetPhongTrongAsync();
            return map.Map<List<Phong_DTO>>(entities);
        }

        public async Task<Phong_DTO?> GetPhongByIdAsync(string maPhong)
        {
            var entity = await dao.GetByIdAsync(maPhong);
            if (entity == null) return null;
            return map.Map<Phong_DTO>(entity);
        }

        public async Task<List<Phong_DTO>> GetByToaNhaAsync(string maToa)
        {
            var entities = await dao.GetByMaToaAsync(maToa);
            return map.Map<List<Phong_DTO>>(entities);
        }
    }
}
