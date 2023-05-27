using System.ComponentModel.DataAnnotations;

namespace io_book_project.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
