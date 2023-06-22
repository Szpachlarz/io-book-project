using io_book_project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace io_book_project.ViewModels
{
    public class BookPageViewModel
    {
        public string Title { get; set; }
        public string? OriginalTitle { get; set; }
        public double ISBN { get; set; }

        // [DisplayFormat(DataFormatString = "{0:yyyy}")]
        public DateTime PublicationYear { get; set; }

        // [DataType(DataType.Date)]
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
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string Publisher { get; set; }
        public int ReviewId { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
    }
}
