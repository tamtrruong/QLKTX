using AutoMapper;
using QLKTX_DAO;
using QLKTX_DAO.Models;
using QLKTX_DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKTX_BUS
{
    public class Auth_BUS
    {
        private readonly TaiKhoan_DAO dao;
        private readonly IMapper map;

        public Auth_BUS(TaiKhoan_DAO dao, IMapper mapper)
        {
            this.dao = dao;
            map = mapper;
        }

        public async Task RegisterAsync(Register_DTO dto)
        {
            var account = map.Map<TaiKhoan>(dto);
            account.MatKhau = BCrypt.Net.BCrypt.HashPassword(dto.MatKhau);

            await dao.AddAccountAsync(account);
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var user = await dao.GetByUsernameAsync(request.Username);
            if (user == null) return null;
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.MatKhau);
            if (!isPasswordValid) return null;
            var response = map.Map<LoginResponse>(user);
            response.Token = "token-jwt-se-tao-o-day";
            return response;
        }
    }
}
