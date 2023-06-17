using System.ComponentModel.DataAnnotations;

namespace io_book_project.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public List<Book> Books { get; set; }

    }
}
