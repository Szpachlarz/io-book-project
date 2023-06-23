using System.ComponentModel.DataAnnotations.Schema;

namespace io_book_project.Models
{
    public class UserFavourite
    {
        public int Id { get; set; }
        [ForeignKey ("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        [ForeignKey ("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
