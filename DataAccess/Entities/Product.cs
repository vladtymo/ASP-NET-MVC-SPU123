using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Product
    {
        /* Data Annotation Attributes: 
         *      Min/MaxLength, StringLength
         *      Required
         *      Range
         *      PhoneNumber, EmailAddress, CreditCard
         *      RegularExpression
         */
        public int Id { get; set; }

        [Required, MinLength(3, ErrorMessage = "Name must has at least 3 characters.")]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        [Range(0, int.MaxValue)]
        public decimal? Discout { get; set; }

        public string? ImageUrl { get; set; }

        public bool InStock { get; set; }

        [StringLength(1000, MinimumLength = 10)]
        public string? Description { get; set; }

        // ---------- navigation properties
        public Category? Category { get; set; }
    }
}
