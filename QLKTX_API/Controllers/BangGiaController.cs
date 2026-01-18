using Microsoft.AspNetCore.Mvc;
using QLKTX_BUS;
using QLKTX_DAO;
using QLKTX_DTO;

namespace QLKTX_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BangGiaController : ControllerBase
    {
        private readonly BangGia_BUS bus;

        public BangGiaController(BangGia_BUS bus)
        {
            this.bus = bus;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await bus.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(BangGia_DTO dto)
        {
            await bus.AddAsync(dto);
            return Ok("Thêm bảng giá thành công");
        }

        [HttpPut]
        public async Task<IActionResult> Update(BangGia_DTO dto)
        {
            bool result = await bus.UpdateAsync(dto);
            if (!result)
                return NotFound("Không tìm thấy bảng giá");

            return Ok("Cập nhật bảng giá thành công");
        }

    }
}
