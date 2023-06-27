using io_book_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace io_book_project.ViewModels
{
    public class PublishersPageViewModel : Controller
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public IEnumerable<Book> Books { get; set; }

    }
}
