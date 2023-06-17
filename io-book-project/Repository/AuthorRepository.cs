using io_book_project.Data;
using io_book_project.Interfaces;
using io_book_project.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace io_book_project.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;
        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }
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
        public async Task<IEnumerable<Author>> GetAuthorNames(int bookId)
        {
            return await _context.Authors
                .Include(i => i.BookAuthors)
                .ThenInclude(i => i.Book)
                .Where(i => i.BookAuthors.Any(ba => ba.BookId == bookId))
                //.Select(i => i.Author)
                .AsNoTracking()
                .OrderBy(i => i.Surname)
                .ToListAsync();
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
