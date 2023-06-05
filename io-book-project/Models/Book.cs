using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace io_book_project.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? OriginalTitle { get; set; }
        public double ISBN { get; set; }        
        public DateTime PublicationYear { get; set; }
        public DateTime? FirstPublicationYear { get; set; }
        public string Language { get; set; }
        public string? OriginalLanguage { get; set; }
        public string? Translation { get; set; }
        public int PageCount { get; set; }
        public string? Series { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CoverImagePath { get; set; }

        //Publisher
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

    }
}
