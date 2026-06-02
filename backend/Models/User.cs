namespace TreatTraderApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string? Phone { get; set; }
        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

