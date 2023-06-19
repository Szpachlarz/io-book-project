using io_book_project.Models;

namespace io_book_project.Interfaces
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<Publisher>> GetAll();

        Task<Publisher?> GetByIdAsync(int id);

        Task<Publisher?> GetByIdAsyncNoTracking(int id);

        Task<int> GetCountAsync();

        Task<Publisher?> GetByBookId(int id);

        bool Add(Publisher publisher);

        bool Update(Publisher publisher);

        bool Delete(Publisher publisher);

        bool Save();
    }
}
