using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLKTX_BUS;
using QLKTX_DTO.Tke; // QUAN TRỌNG: Namespace chứa DTO ViPham
using System.Security.Claims;

namespace QLKTX_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Bắt buộc đăng nhập mới được gọi
    public class ViPhamController : ControllerBase
    {
        private readonly ViPham_BUS _bus;

        public ViPhamController(ViPham_BUS bus)
        {
            _bus = bus;
        }

        // =============================================================
        // Nghiệp vụ 3: Tra cứu lịch sử vi phạm
        // GET: api/ViPham?maSV=SV001 (Admin) hoặc api/ViPham (Sinh viên tự xem)
        // =============================================================
        [HttpGet]
        public async Task<IActionResult> GetViolations([FromQuery] string? maSV)
        {
            // 1. Lấy thông tin người dùng từ Token
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            // Lưu ý: Lúc làm Login, bạn lưu MaSV vào Claim nào? (Name hay NameIdentifier?)
            // Thường là ClaimTypes.Name. Nếu null thì check lại Auth_BUS nhé.
            var currentUserId = User.FindFirst(ClaimTypes.Name)?.Value;

            string maSV_CanXem = maSV;

            if (role == "SinhVien")
            {
                // Nếu là SV: Bắt buộc đè MaSV thành ID của chính mình (Không cho xem của người khác)
                maSV_CanXem = currentUserId;
            }
            // Nếu là Admin: Giữ nguyên maSV truyền vào (để xem lịch sử của SV bất kỳ)

            try
            {
                // Gọi hàm XemLichSu bên BUS
                var result = await _bus.XemLichSu(maSV_CanXem);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // =============================================================
        // Nghiệp vụ 9: Ghi nhận vi phạm mới & Xử phạt tự động
        // POST: api/ViPham
        // =============================================================
        [HttpPost]
        [Authorize(Roles = "Admin")] // Chỉ Admin mới được quyền phạt
        public async Task<IActionResult> CreateViolation([FromBody] CreateViPham_DTO dto)
        {
            try
            {
                // Gọi hàm GhiNhanViPham bên BUS
                var result = await _bus.GhiNhanViPham(dto);

                // Trả về thông báo kết quả (Ví dụ: "Đã chấm dứt hợp đồng!")
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi ghi nhận: " + ex.Message });
            }
        }
    }
}