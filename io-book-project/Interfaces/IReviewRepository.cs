using io_book_project.Models;

namespace io_book_project.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAll();
        Task<Review?> GetByIdAsync(int id);
    }
}
