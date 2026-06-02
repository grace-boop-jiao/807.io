using System.IO;
using Microsoft.EntityFrameworkCore;
using TreatTraderApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. 設定 SQLite 路徑（就是你專案根目錄的 treattrader.db）
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "treattrader_v2.db");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// 2. 加入 Controllers
builder.Services.AddControllers();

// 3. 加入 CORS（讓前端網頁可以呼叫 API）
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// 4. Swagger 相關註冊
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 5. 啟動時自動套用 Migration（沒有 DB 就會幫你建）
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();   // ✅ 沒有表就幫你建立
}

// 6. ⭐ 不管是 Development / Production，通通啟用 Swagger
app.UseSwagger();
app.UseSwaggerUI();

// 7. 其他中介軟體
app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

// 8. 對應 Controllers
app.MapControllers();

app.Run();



