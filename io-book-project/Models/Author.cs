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

        public string DateOfBirth { get; set; }

        public string? DateOfDeath { get; set; }

        //Authors
        public List<BookAuthor> BookAuthors { get; set; }
    }
}
