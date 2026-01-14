using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLKTX_BUS;
using QLKTX_DTO.Bill;

namespace QLKTX_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Ch? Admin m?i ???c nh?p ?i?n n??c
    public class DienNuocController : ControllerBase
    {
        private readonly DienNuoc_BUS _bus;

        public DienNuocController(DienNuoc_BUS bus)
        {
            _bus = bus;
        }

        // POST: api/DienNuoc/record
        [HttpPost("record")]
        public async Task<IActionResult> GhiChiSo([FromBody] CreateHD_DTO dto)
        {
            // L?u ý: BUS c?a b?n ?ang dùng chung CreateHD_DTO cho c? vi?c ghi ch? s?
            // Th?c t? nên tách ra DienNuoc_DTO riêng, nh?ng dùng t?m c?ng ???c
            try
            {
                await _bus.GhiChiSoAsync(dto);
                return Ok("Ghi chỉ số điện nước thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}