using io_book_project.Data;
using io_book_project.Interfaces;
using io_book_project.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

namespace io_book_project.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Add(User user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User user)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Book>> GetAllFavourites(string userId)
        {
            return await _context.Books
                .Include(i => i.UserFavourites)
                .ThenInclude(i => i.User)
                .Where(i => i.UserFavourites.Any(ba => ba.UserId == userId))
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetAFewFavourites(string userId)
        {
            return await _context.Books
                .Include(i => i.UserFavourites)
                .ThenInclude(i => i.User)
                .Where(i => i.UserFavourites.Any(ba => ba.UserId == userId))
                .Take(5)
                .AsNoTracking()
                .ToListAsync();
        }
        public bool CheckIfItIsAlreadyFavourite(string userId, int bookId)
        {
            return _context.UserFavourites.Any(i => i.BookId == bookId && i.UserId == userId);
        }
        public bool AddFavourite(UserFavourite userFavourite)
        {
            _context.Add(userFavourite);
            return Save();
        }
        public bool RemoveFavourite(string userId, int bookId)
        {
            var itemToRemove = _context.UserFavourites.FirstOrDefault(i => i.BookId == bookId && i.UserId == userId);
            if (itemToRemove != null)
            {
                _context.Remove(itemToRemove);
            }
            return Save();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByName(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(i => i.UserName == name);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(User user)
        {
            _context.Update(user);
            return Save();
        }

        public async Task<IEnumerable<User>> GetAllForThisBook(int bookId)
        {
            return await _context.Reviews
            .Include(i => i.User)
            .Where(i => i.BookId == bookId)
            .Select(i => new User
            {
                UserName = i.User.UserName,
            })
            .ToListAsync();
        }
    }
}
