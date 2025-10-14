namespace LuxifyBackend.API.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string VerificationStatus { get; set; } // Pending/Verified/Rejected
    }
}
