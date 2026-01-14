using Microsoft.AspNetCore.Mvc;
using QLKTX_BUS;
using QLKTX_DTO.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly Auth_BUS _authBus;
    private readonly IConfiguration _config;

    public AuthController(Auth_BUS authBus, IConfiguration config)
    {
        _authBus = authBus;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _authBus.LoginAsync(request);
        if (user == null) return Unauthorized("Sai tài khoản hoặc mật khẩu");

        // Logic t?o JWT Token th?t
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.TenDangNhap),
                new Claim(ClaimTypes.Role, (int)user.Quyen == 0 ? "Admin" : "SinhVien")
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        user.Token = tokenHandler.WriteToken(token);

        return Ok(user);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Register_DTO dto)
    {
        try
        {
            await _authBus.RegisterAsync(dto);
            return Ok("Đăng ký thành công");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}