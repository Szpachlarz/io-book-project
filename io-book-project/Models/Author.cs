using System.ComponentModel.DataAnnotations;
namespace io_book_project.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string Names { get; set; }
        public string? Surname { get; set; }

        public string Nationality { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public DateOnly? DateOfDeath { get; set; }
    }
}
