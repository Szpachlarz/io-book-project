using io_book_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace io_book_project.ViewModels
{
    public class AuthorsPageViewModel : Controller
    {
        public string Names { get; set; }
        public string? Surname { get; set; }

        public string Nationality { get; set; }

        public string DateOfBirth { get; set; }

        public string? DateOfDeath { get; set; }

        //Authors
        public List<BookAuthor> BookAuthors { get; set; }
    }
}
