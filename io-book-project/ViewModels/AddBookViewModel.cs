using io_book_project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace io_book_project.ViewModels
{
    public class AddBookViewModel
    {
        [Display(Name = "Tytuł")]
        [Required(ErrorMessage = "Nazwa książki jest wymagana")]
        [StringLength(120, ErrorMessage = "Maksymalnie 120 znaków")]
        public string Title { get; set; }
        public string? OriginalTitle { get; set; }
        public double ISBN { get; set; }
        public DateTime PublicationYear { get; set; }
        public DateTime FirstPublicationYear { get; set; }
        public string Language { get; set; }
        public string? OriginalLanguage { get; set; }
        public string? Translation { get; set; }
        public int PageCount { get; set; }
        public string? Series { get; set; }
        public string Description { get; set; }
        public string CoverImagePath { get; set; }

        //Publisher
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        //Author
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        //Category
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
