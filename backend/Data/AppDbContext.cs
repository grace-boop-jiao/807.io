using Microsoft.EntityFrameworkCore;
using TreatTraderApi.Models;

namespace TreatTraderApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Review> Reviews => Set<Review>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Email 唯一
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Review 關聯 Product
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId);

            // Review 關聯 User
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);

            // ---- 這裡是 Products 的種子資料 ----
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "日本靜岡抹茶蛋糕",
                    Price = 350,
                    Category = "japan",
                    Desc = "來自靜岡的濃郁抹茶香，口感綿密。",
                    Nutrition = "熱量: 350大卡 | 蛋白質: 5g",
                    Img = "Matcha.jpg"
                },
                new Product
                {
                    Id = 2,
                    Name = "美國重磅巧克力餅乾",
                    Price = 150,
                    Category = "usa",
                    Desc = "美式經典，外酥內軟，巧克力豆爆漿。",
                    Nutrition = "熱量: 450大卡 | 糖: 30g",
                    Img = "Cookie.jpg"
                },
                new Product
                {
                    Id = 3,
                    Name = "法國巴黎馬卡龍",
                    Price = 450,
                    Category = "france",
                    Desc = "法式浪漫，甜而不膩，少女心爆發。",
                    Nutrition = "熱量: 200大卡 | 糖: 40g",
                    Img = "Macaron.jpg"
                },
                new Product
                {
                    Id = 4,
                    Name = "英國皇家伯爵茶餅",
                    Price = 280,
                    Category = "uk",
                    Desc = "下午茶首選，優雅茶香。",
                    Nutrition = "熱量: 150大卡 | 脂肪: 10g",
                    Img = "TeaBiscuit.jpg"
                },
                new Product
                {
                    Id = 5,
                    Name = "韓國起司夾心餅",
                    Price = 120,
                    Category = "korea",
                    Desc = "鹹甜交織，起司控最愛。",
                    Nutrition = "熱量: 300大卡 | 鈉: 200mg",
                    Img = "Cheese.jpg"
                },
                new Product
                {
                    Id = 6,
                    Name = "日本白色戀人",
                    Price = 480,
                    Category = "japan",
                    Desc = "北海道經典伴手禮。",
                    Nutrition = "熱量: 250大卡 | 糖: 15g",
                    Img = "WhiteLover.jpg"
                },
                new Product
                {
                    Id = 7,
                    Name = "紐約起司蛋糕",
                    Price = 320,
                    Category = "usa",
                    Desc = "重乳酪風味，濃郁化口。",
                    Nutrition = "熱量: 400大卡 | 脂肪: 25g",
                    Img = "CheeseCake.jpg"
                },
                new Product
                {
                    Id = 8,
                    Name = "法國檸檬塔",
                    Price = 160,
                    Category = "france",
                    Desc = "酸甜清爽，法式甜點代表。",
                    Nutrition = "熱量: 220大卡 | 糖: 18g",
                    Img = "LemonTart.jpg"
                },
                new Product
                {
                    Id = 9,
                    Name = "德國黑森林蛋糕",
                    Price = 500,
                    Category = "germany",
                    Desc = "酒漬櫻桃與巧克力的完美結合。",
                    Nutrition = "熱量: 450大卡 | 酒精: 微量",
                    Img = "Cake.jpg"
                },
                new Product
                {
                    Id = 10,
                    Name = "義大利提拉米蘇",
                    Price = 380,
                    Category = "italy",
                    Desc = "帶我走，享受微醺的咖啡香。",
                    Nutrition = "熱量: 380大卡 | 咖啡因: 有",
                    Img = "Tiramisu.jpg"
                },
                new Product
                {
                    Id = 11,
                    Name = "比利時巧克力",
                    Price = 250,
                    Category = "belgium",
                    Desc = "頂級可可，絲滑柔順。",
                    Nutrition = "熱量: 550大卡 | 可可脂: 高",
                    Img = "Choco.jpg"
                },
                new Product
                {
                    Id = 12,
                    Name = "台灣鳳梨酥",
                    Price = 200,
                    Category = "taiwan",
                    Desc = "在地土鳳梨，酸甜適中。",
                    Nutrition = "熱量: 180大卡 | 纖維: 2g",
                    Img = "Pineapple.jpg"
                }
            ); // 👈 注意這裡要有 ); 結束 HasData
        }
    }
}


