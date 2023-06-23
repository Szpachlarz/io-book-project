using io_book_project.Models;

namespace io_book_project.ViewModels
{
    public class UserPageViewModel
    {
        public IEnumerable<Book> FavouriteBooks { get; set; }
    }
}
