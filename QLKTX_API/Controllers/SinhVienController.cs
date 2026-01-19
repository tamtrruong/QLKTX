using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLKTX_BUS;
using QLKTX_DAO.Models;
using QLKTX_DTO.SV;

namespace QLKTX_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SinhVienController : ControllerBase
    {
        private readonly Sinhvien_BUS _bus;

        public SinhVienController(Sinhvien_BUS bus)
        {
            _bus = bus;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _bus.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var sv = await _bus.GetByIdAsync(id);
            if (sv == null) return NotFound();
            return Ok(sv);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateSV_DTO dto)
        {
            try
            {
                await _bus.CreateAsync(dto);
                return StatusCode(201, "Thêm sinh viên thành công");
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] SinhVien_DTO dto)
        {
            await _bus.UpdateAsync(dto);
            return Ok("Cập nhật thành công");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            await _bus.DeleteAsync(id);
            return Ok("Xóa thành công");
        }

        [AllowAnonymous]
        [HttpGet("test-db")]
        public IActionResult TestDb([FromServices] QLKTXContext db)
        {
            try
            {
                db.Database.CanConnectAsync();
                return Ok("DB CONNECT OK");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}