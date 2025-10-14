using System.ComponentModel.DataAnnotations;

namespace LuxifyBackend.API.DTOs
{
    public class CreateProductDto
    {
        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required, MaxLength(2000)]
        public string Description { get; set; }

        [Range(0, 1_000_000)]
        public decimal Price { get; set; }

        [Required, MaxLength(40)]
        public string Category { get; set; }

        public string ImageUrl { get; set; } // mockad bilduppladdning
        public string Condition { get; set; } = "Used"; // New/Used/etc.
        public bool Negotiable { get; set; } = false;   // pris förhandlingsbart
    }
}
