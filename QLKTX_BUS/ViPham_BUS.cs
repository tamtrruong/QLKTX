using AutoMapper;
using QLKTX_DAO;
using QLKTX_DAO.Models;
using QLKTX_DTO.Tke;

namespace QLKTX_BUS
{
    public class ViPham_BUS
    {
        private readonly ViPham_DAO _vpDao;
        private readonly HopDong_DAO _hdDao; // Cần cái này để xử trảm hợp đồng
        private readonly IMapper _mapper;

        public ViPham_BUS(ViPham_DAO vpDao, HopDong_DAO hdDao, IMapper mapper)
        {
            _vpDao = vpDao;
            _hdDao = hdDao;
            _mapper = mapper;
        }

        public async Task<string> GhiNhanViPham(CreateViPham_DTO dto)
        {
            // 1. Lưu vi phạm
            var entity = _mapper.Map<ViPham>(dto);
            await _vpDao.CreateViPham(entity);

            // 2. LOGIC XỬ PHẠT TỰ ĐỘNG
            string thongBao = "Đã ghi nhận vi phạm.";

            // Trường hợp 1: Mức độ NẶNG (2) -> Đuổi luôn
            if (dto.MucDo == MucDoViPham.Nang)
            {
                // Gọi hàm thanh lý hợp đồng mà bạn đã làm ở bài trước
                await ThanhLyHopDongCuaSV(dto.MaSV);
                thongBao += " Mức độ NẶNG -> Hệ thống đã tự động CHẤM DỨT HỢP ĐỒNG!";
            }
            // Trường hợp 2: Mức độ TRUNG BÌNH (1)
            else if (dto.MucDo == MucDoViPham.TrungBinh)
            {
                // Đếm số lần vi phạm Trung bình
                int soLanBiPhat = await _vpDao.CountViPhamByUser(dto.MaSV, MucDoViPham.TrungBinh);

                if (soLanBiPhat >= 3)
                {
                    await ThanhLyHopDongCuaSV(dto.MaSV);
                    thongBao += $" Đây là lần thứ {soLanBiPhat} vi phạm Trung bình -> Hệ thống đã tự động CHẤM DỨT HỢP ĐỒNG!";
                }
                else
                {
                    thongBao += $" Đây là lần thứ {soLanBiPhat} vi phạm Trung bình. (Quá 3 lần sẽ bị buộc thôi học).";
                }
            }
            // Trường hợp 3: Nhẹ -> Chỉ nhắc nhở
            else
            {
                thongBao += " Hình thức: Nhắc nhở.";
            }

            return thongBao;
        }

        // Hàm phụ trợ: Tìm hợp đồng đang ở và cắt
        private async Task ThanhLyHopDongCuaSV(string maSV)
        {
            // Tìm hợp đồng đang hiệu lực của SV này
            // (Lưu ý: Bạn cần viết thêm hàm GetHopDongActiveBySV bên HopDong_DAO nếu chưa có)
            // Tạm thời mình giả định bạn sẽ dùng mã hợp đồng để xóa, hoặc logic tìm ID ở đây.

            // Cách đơn giản nhất: 
            // 1. Tìm danh sách hợp đồng của SV này
            var listHD = await _hdDao.GetAllHopDongs(); // Hoặc viết hàm tìm riêng cho nhẹ
            var hdActive = listHD.FirstOrDefault(x => x.MaSv == maSV && x.TinhTrang == 0);

            if (hdActive != null)
            {
                await _hdDao.ThanhLyHopDong(hdActive.MaHopDong);
            }
        }

        public async Task<List<ViPham_DTO>> XemLichSu(string maSV)
        {
            var list = await _vpDao.GetHistory(maSV);
            return _mapper.Map<List<ViPham_DTO>>(list);
        }
    }
}