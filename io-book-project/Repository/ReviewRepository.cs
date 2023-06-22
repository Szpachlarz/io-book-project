using io_book_project.Data;
using io_book_project.Interfaces;
using io_book_project.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace io_book_project.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;
        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Add(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool Delete(Review review)
        {
            _context.Remove(review);
            return Save();
        }

        public async Task<IEnumerable<Review>> GetAll()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetAllForThisBook(int bookId)
        {
            return await _context.Reviews
                .Where(i => i.BookId == bookId)
                .ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Review review)
        {
            _context.Update(review);
            return Save();
        }
    }
}
