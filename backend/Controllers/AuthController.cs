using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreatTraderApi.Data;
using TreatTraderApi.Dtos;
using TreatTraderApi.Models;

namespace TreatTraderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AuthController(AppDbContext db)
        {
            _db = db;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 檢查 Email 是否已存在
            bool exists = await _db.Users.AnyAsync(u => u.Email == dto.Email);
            if (exists)
            {
                return BadRequest(new { message = "此 Email 已註冊過。" });
            }

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password, // 正式系統要做加密，這邊先練習
                Phone = dto.Phone,
                Address = dto.Address
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                user.Id,
                user.Name,
                user.Email,
                user.Phone,
                user.Address
            });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password);

            if (user == null)
            {
                return BadRequest(new { message = "帳號或密碼錯誤。" });
            }

            return Ok(new
            {
                user.Id,
                user.Name,
                user.Email,
                user.Phone,
                user.Address
            });
        }
    }
}

