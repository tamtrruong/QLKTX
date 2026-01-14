using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models; // Dùng namespace này cho ngắn gọn
using QLKTX_BUS;
using QLKTX_BUS.Mappings;
using QLKTX_DAO;
using QLKTX_DAO.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ====================================================
// 1. CẤU HÌNH DATABASE
// ====================================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<QLKTXContext>(options =>
    options.UseSqlServer(connectionString));

// ====================================================
// 2. CẤU HÌNH AUTOMAPPER
// ====================================================
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// ====================================================
// 3. ĐĂNG KÝ DEPENDENCY INJECTION (DAO & BUS)
// ====================================================
// --- DAO ---
builder.Services.AddScoped<Sinhvien_DAO>();
builder.Services.AddScoped<Phong_DAO>();
builder.Services.AddScoped<HopDong_DAO>();
builder.Services.AddScoped<HoaDon_DAO>();
builder.Services.AddScoped<DienNuoc_DAO>();
builder.Services.AddScoped<TaiKhoan_DAO>();
builder.Services.AddScoped<ThongKe_DAO>();
builder.Services.AddScoped<ViPham_DAO>();

// --- BUS ---
builder.Services.AddScoped<Sinhvien_BUS>();
builder.Services.AddScoped<Phong_BUS>();
builder.Services.AddScoped<HopDong_BUS>();
builder.Services.AddScoped<HoaDon_BUS>();
builder.Services.AddScoped<DienNuoc_BUS>();
builder.Services.AddScoped<Auth_BUS>();
builder.Services.AddScoped<Thongke_BUS>();
builder.Services.AddScoped<ViPham_BUS>();


// ====================================================
// 4. CẤU HÌNH JWT AUTHENTICATION
// ====================================================
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// ====================================================
// 5. CẤU HÌNH CONTROLLERS & SWAGGER (Kèm nút ổ khóa)
// ====================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "QLKTX API", Version = "v1" });

    // Cấu hình nút "Authorize" (Ổ khóa) trên Swagger
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Nhập Token vào đây. Ví dụ: Bearer eyJhbGciOiJIUz...",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// ====================================================
// 6. CẤU HÌNH CORS (Cho phép Frontend gọi vào)
// ====================================================
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// ====================================================
// PIPELINE XỬ LÝ REQUEST
// ====================================================

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.UseCors("AllowAll"); // Bật CORS

// Thứ tự bắt buộc: Xác thực (Ai đây?) -> Phân quyền (Được làm gì?)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();