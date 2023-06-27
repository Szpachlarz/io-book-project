using io_book_project.Models;

namespace io_book_project.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAll();

        Task<IEnumerable<Book>> GetAllbooks();

        Task<Book?> GetByIdAsync(int id);

        Task<Book?> GetByIdAsyncNoTracking(int id);

        Task<int> GetCountAsync();

        //Task<int> GetCountByCategoryAsync(ClubCategory category);

        bool Add(Book book);

        bool Update(Book book);

        bool Delete(Book book);

        bool Save();
    }
}
