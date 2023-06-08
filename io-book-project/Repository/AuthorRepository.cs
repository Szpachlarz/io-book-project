using io_book_project.Data;
using io_book_project.Interfaces;
using io_book_project.Models;
using Microsoft.EntityFrameworkCore;

namespace io_book_project.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;
        public bool Add(Author author)
        {
            _context.Add(author);
            return Save();
        }

        public bool Delete(Author author)
        {
            _context.Remove(author);
            return Save();
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Authors.CountAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Author author)
        {
            _context.Update(author);
            return Save();
        }
    }
}
