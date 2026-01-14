using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLKTX_BUS;
using QLKTX_DTO.Bill;

namespace QLKTX_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class HoaDonController : ControllerBase
	{
		private readonly HoaDon_BUS _bus;

		public HoaDonController(HoaDon_BUS bus)
		{
			_bus = bus;
		}

		// 1. Admin b?m nút "Tính ti?n" hàng tháng
		// POST: api/HoaDon/generate
		[HttpPost("generate")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateMonthlyBill([FromBody] CreateHD_DTO dto)
		{
			try
			{
				await _bus.CreateHoaDonAsync(dto);
				return Ok("Đã xuất hóa đơn thành công.");
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
        // 2. Sinh viên thanh toán
        // PATCH: api/HoaDon/{maHD}/pay
        [HttpPatch("{maHoaDon}/pay")]
        public async Task<IActionResult> ThanhToan(string maHoaDon, [FromBody] PaymentMethod method)
        {
            try
            {
                // SỬA LỖI: Ép kiểu (int) trước biến method
                await _bus.ThanhToanAsync(maHoaDon, (int)method);

                return Ok(new { message = "Thanh toán thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // 3. Xem danh sách n? (Admin)
        [HttpGet("chua-thanh-toan")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetUnpaid()
		{
			return Ok(await _bus.GetHoaDonChuaThanhToanAsync());
		}

		// 4. Xem l?ch s? hóa ??n c?a 1 phòng
		[HttpGet("history/{maPhong}")]
		public async Task<IActionResult> GetHistory(string maPhong)
		{
			return Ok(await _bus.GetLichSuHoaDonAsync(maPhong));
		}
	}
}