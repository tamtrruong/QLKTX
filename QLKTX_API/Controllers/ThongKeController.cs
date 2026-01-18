using Microsoft.AspNetCore.Mvc;
using QLKTX_BUS;

namespace QLKTX_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly Thongke_BUS bus;

        public ThongKeController(Thongke_BUS bus)
        {
            this.bus = bus;
        }

        [HttpGet("doanh-thu-nam/{nam}")]
        public async Task<IActionResult> GetDoanhThuNam(int nam)
        {
            if (nam < 2000)
                return BadRequest("Năm không hợp lệ");

            var result = await bus.GetTongDoanhThuNamAsync(nam);
            return Ok(result);
        }

        [HttpGet("trang-thai-hoa-don")]
        public async Task<IActionResult> GetThongKeTrangThaiHoaDon()
        {
            var result = await bus.GetTyLeThanhToanAsync();
            return Ok(result);
        }

        [HttpGet("doanh-thu-phong")]
        public async Task<IActionResult> GetDoanhThuTheoPhong(
            [FromQuery] DateTime tuNgay,
            [FromQuery] DateTime denNgay)
        {
            if (tuNgay > denNgay)
                return BadRequest("Từ ngày phải nhỏ hơn hoặc bằng đến ngày");

            var result = await bus.GetDoanhThuTheoPhongAsync(tuNgay, denNgay);
            return Ok(result);
        }
    }
}
