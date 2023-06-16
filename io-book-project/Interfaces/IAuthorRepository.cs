using io_book_project.Models;

namespace io_book_project.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAll();

        Task<Author?> GetByIdAsync(int id);

        Task<int> GetCountAsync();
        Task<IEnumerable<Author>> GetAuthorNames(int id);
        bool Add(Author author);

        bool Update(Author author);

        bool Delete(Author author);

        bool Save();
    }
}
