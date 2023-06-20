using io_book_project.Models;

namespace io_book_project.ViewModels
{
    public class BookListViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public List<List<Author>> Authors { get; set; }
        public List<Book> Data { get; set; }
    }
}
