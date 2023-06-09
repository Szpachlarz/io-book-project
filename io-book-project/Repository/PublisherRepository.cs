using io_book_project.Data;
using io_book_project.Interfaces;
using io_book_project.Models;
using Microsoft.EntityFrameworkCore;

namespace io_book_project.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _context;
        public bool Add(Publisher publisher)
        {
            _context.Add(publisher);
            return Save();
        }

        public bool Delete(Publisher publisher)
        {
            _context.Remove(publisher);
            return Save();
        }

        public async Task<IEnumerable<Publisher>> GetAll()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<Publisher?> GetByIdAsync(int id)
        {
            return await _context.Publishers.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Publishers.CountAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Publisher publisher)
        {
            _context.Update(publisher);
            return Save();
        }
    }
}
