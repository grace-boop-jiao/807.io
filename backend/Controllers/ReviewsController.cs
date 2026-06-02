using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreatTraderApi.Data;
using TreatTraderApi.Dtos;
using TreatTraderApi.Models;

namespace TreatTraderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ReviewsController(AppDbContext db)
        {
            _db = db;
        }

        // 取得某商品的評論（含簡單分頁）
        // GET: api/reviews/product/3?page=1&pageSize=5
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetByProduct(
            int productId,
            int page = 1,
            int pageSize = 5)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 5;

            var query = _db.Reviews
                .Where(r => r.ProductId == productId)
                .OrderByDescending(r => r.CreatedAt);

            var total = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new
                {
                    r.Id,
                    r.UserName,
                    r.Rating,
                    r.Content,
                    Date = r.CreatedAt.ToString("yyyy/MM/dd")
                })
                .ToListAsync();

            return Ok(new { total, items });
        }

        // 新增評論
        // POST: api/reviews
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 檢查商品是否存在
            var productExists = await _db.Products.AnyAsync(p => p.Id == dto.ProductId);
            if (!productExists)
            {
                return BadRequest(new { message = $"無此商品 ProductId = {dto.ProductId}" });
            }

            // 檢查使用者是否存在
            var userExists = await _db.Users.AnyAsync(u => u.Id == dto.UserId);
            if (!userExists)
            {
                return BadRequest(new { message = $"無此會員 UserId = {dto.UserId}" });
            }

            // 建立評論資料
            var review = new Review
            {
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                UserName = dto.UserName,
                Rating = dto.Rating,
                Content = dto.Content,
                CreatedAt = DateTime.Now
            };

            _db.Reviews.Add(review);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                review.Id,
                review.ProductId,
                review.UserName,
                review.Rating,
                review.Content,
                Date = review.CreatedAt.ToString("yyyy/MM/dd")
            });
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            // 確認這個會員存在（可選，但比較安全）
            var userExists = await _db.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
            {
                return BadRequest(new { message = $"無此會員 UserId = {userId}" });
            }

            var list = await _db.Reviews
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new
                {
                    r.Id,
                    r.ProductId,
                    ProductName = r.Product != null ? r.Product.Name : "",
                    r.Rating,
                    r.Content,
                    Date = r.CreatedAt.ToString("yyyy/MM/dd")
                })
                .ToListAsync();

            return Ok(list);
        }


    }
}

