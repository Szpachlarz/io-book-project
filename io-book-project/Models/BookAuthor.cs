using System.ComponentModel.DataAnnotations;
namespace io_book_project.Models
{
    public class BookAuthor
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }

    }
}
