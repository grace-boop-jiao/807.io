namespace TreatTraderApi.Models
{
    public class Product
    {
        public int Id { get; set; }      // 對應 products.json 的 id
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public string Category { get; set; } = null!;
        public string Desc { get; set; } = null!;
        public string Nutrition { get; set; } = null!;
        public string Img { get; set; } = null!;

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}

