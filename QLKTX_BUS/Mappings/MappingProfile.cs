using AutoMapper;
using QLKTX_DAO.Models;
using QLKTX_DTO.Auth;
using QLKTX_DTO.Bill;
using QLKTX_DTO.Hopdong;

using QLKTX_DTO.Rooms;
using QLKTX_DTO.SV;
using QLKTX_DTO.Tke;


namespace QLKTX_BUS.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // =========================================================
            // 1. SINH VIÊN (Đã chuẩn)
            // =========================================================
            CreateMap<CreateSV_DTO, SinhVien>()
                .ForMember(dest => dest.NgaySinh, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.NgaySinh)))
                .ForMember(dest => dest.GioiTinh, opt => opt.MapFrom(src => src.GioiTinh == "Nam" ? (byte)1 : (byte)0));

            CreateMap<SinhVien, SinhVien_DTO>()
                .ForMember(dest => dest.NgaySinh, opt => opt.MapFrom(src => src.NgaySinh.HasValue
                    ? src.NgaySinh.Value.ToDateTime(TimeOnly.MinValue)
                    : default(DateTime?)))
                .ForMember(dest => dest.GioiTinh, opt => opt.MapFrom(src => src.GioiTinh == 1 ? "Nam" : "Nữ"));


            // =========================================================
            // 2. PHÒNG & TÒA NHÀ (Đã chuẩn)
            // =========================================================
            CreateMap<Phong, Phong_DTO>().ReverseMap();
            CreateMap<ToaNha, ToaNha_DTO>().ReverseMap();

            // =========================================================
            // 3. HỢP ĐỒNG (CẬP NHẬT MỚI 🚨)
            // =========================================================

            // Map từ Entity -> DTO (Hiển thị danh sách)
            CreateMap<HopDong, HopDong_DTO>()
                // Lấy Họ tên SV từ bảng SinhVien thông qua Navigation Property
                .ForMember(dest => dest.HoTenSV, opt => opt.MapFrom(src => src.MaSvNavigation.HoTen))
                // Lấy Tên Phòng từ bảng Phong
                .ForMember(dest => dest.TenPhong, opt => opt.MapFrom(src => src.MaPhongNavigation.TenPhong))
                // Chuyển đổi trạng thái từ số sang Enum (nếu cần)
                .ForMember(dest => dest.TinhTrang, opt => opt.MapFrom(src => (TinhTrangHopDong)src.TinhTrang));

            // Map từ DTO -> Entity (Tạo mới)
            CreateMap<CreateHopDong_DTO, HopDong>()
                .ForMember(dest => dest.NgayBatDau, opt => opt.MapFrom(src => src.NgayBatDau));

            CreateMap<GiaHanHopDong_DTO, HopDong>();


            // =========================================================
            // 4. HÓA ĐƠN & ĐIỆN NƯỚC (Đã chuẩn)
            // =========================================================
            CreateMap<CreateHD_DTO, DienNuoc>();

            CreateMap<HoaDon, HoaDon_DTO>()
                .ForMember(dest => dest.TenPhong, opt => opt.MapFrom(src => src.MaPhongNavigation.TenPhong));

            CreateMap<CreateHD_DTO, HoaDon>()
                .ForMember(dest => dest.MaPhong, opt => opt.MapFrom(src => src.MaPhong));


            // =========================================================
            // 5. VI PHẠM & TÀI KHOẢN (Đã chuẩn)
            // =========================================================
            CreateMap<CreateViPham_DTO, ViPham>();

            // Map từ Entity -> DTO hiển thị
            CreateMap<ViPham, ViPham_DTO>()
                .ForMember(dest => dest.HoTenSV, opt => opt.MapFrom(src => src.MaSvNavigation.HoTen))
                // Map Enum sang số (hoặc ngược lại tùy nhu cầu, AutoMapper thường tự lo được cái này)
                .ForMember(dest => dest.MucDo, opt => opt.MapFrom(src => (MucDoViPham)src.MucDo));
            CreateMap<ViPham, ViPham_DTO>().ReverseMap();
            CreateMap<LoginRequest, TaiKhoan>();
            CreateMap<TaiKhoan, LoginResponse>();
            CreateMap<Register_DTO, TaiKhoan>()
                .ForMember(dest => dest.Quyen,  opt => opt.MapFrom(src => (byte)src.VaiTro));

        }
    }
}