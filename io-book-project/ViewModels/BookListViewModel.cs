using io_book_project.Models;

namespace io_book_project.ViewModels
{
    public class BookListViewModel
    {
        public IEnumerable<Book> Books;
        public List<List<Author>> Authors { get; set; }
    }
}
