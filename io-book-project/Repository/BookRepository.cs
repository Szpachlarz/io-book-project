using io_book_project.Data;
using io_book_project.Interfaces;
using io_book_project.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace io_book_project.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context) 
        {
            _context = context;
        }
        public bool Add(Book book)
        {
            _context.Add(book);
            return Save();
        }

        public bool Delete(Book book)
        {
            _context.Remove(book);
            return Save();
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Book?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Books.AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Book book)
        {
            _context.Update(book);
            return Save();
        }
        public async Task<int> GetCountAsync()
        {
            return await _context.Books.CountAsync();
        }

        public Task<IEnumerable<Book>> GetAllbooks()
        {
            throw new NotImplementedException();
        }
    }
}
