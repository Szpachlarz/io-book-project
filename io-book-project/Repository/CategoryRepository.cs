using io_book_project.Data;
using io_book_project.Interfaces;
using io_book_project.Models;
using Microsoft.EntityFrameworkCore;

namespace io_book_project.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public bool Add(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool Delete(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Categories.CountAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
