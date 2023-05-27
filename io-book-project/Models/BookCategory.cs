using System.ComponentModel.DataAnnotations;
namespace io_book_project.Models
{
    public class BookCategory
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CategoryId { get; set; }
    }
}
