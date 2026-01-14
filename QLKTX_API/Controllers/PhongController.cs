using Microsoft.AspNetCore.Mvc;
using QLKTX_BUS;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class PhongController : ControllerBase
{
    private readonly Phong_BUS _phongBus;

    public PhongController(Phong_BUS phongBus)
    {
        _phongBus = phongBus;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _phongBus.GetAllAsync());
    }

    [HttpGet("trong")]
    public async Task<IActionResult> GetPhongTrong()
    {
        return Ok(await _phongBus.GetPhongTrongAsync());
    }

    [HttpGet("toa-nha/{maToa}")]
    public async Task<IActionResult> GetByToa(string maToa)
    {
        return Ok(await _phongBus.GetByToaNhaAsync(maToa));
    }
}