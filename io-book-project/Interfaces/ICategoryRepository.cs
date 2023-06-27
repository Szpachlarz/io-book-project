using io_book_project.Models;

namespace io_book_project.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();

        Task<Category?> GetByIdAsync(int id);

        Task<Category?> GetByIdAsyncNoTracking(int id);

        Task<int> GetCountAsync();

        Task<IEnumerable<Category>> GetCategoryNames(int id);
        Task<int> GetIdByName(string name);

        bool Add(Category category);

        bool Update(Category category);

        bool Delete(Category category);

        bool Save();
    }
}
