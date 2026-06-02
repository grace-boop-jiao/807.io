namespace TreatTraderApi.Dtos
{
    public class CreateReviewDto
    {
        
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public int Rating { get; set; }       // 1~5 星
        public string Content { get; set; } = null!;
    }
}

