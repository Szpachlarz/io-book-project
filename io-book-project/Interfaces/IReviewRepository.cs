using io_book_project.Models;

namespace io_book_project.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAll();

        Task<IEnumerable<Review>> GetAllForThisBook(int id);

        Task<Review?> GetByIdAsync(int id);
        Task<IEnumerable<Review>> GetAllReviews(string userId);
        Task<IEnumerable<Review>> GetAFewReviews(string userId);

        bool Add(Review review);

        bool Update(Review review);

        bool Delete(Review review);

        bool Save();
    }
}
