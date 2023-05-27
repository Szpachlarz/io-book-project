using System.ComponentModel.DataAnnotations;

namespace io_book_project.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
