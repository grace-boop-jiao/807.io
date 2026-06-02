namespace TreatTraderApi.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        // ⚠️ 改成 nullable，並且在 OnModelCreating 裡可以先不設定 FK
        public int? UserId { get; set; }
        public User? User { get; set; }

        public string UserName { get; set; } = null!;
        public int Rating { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
