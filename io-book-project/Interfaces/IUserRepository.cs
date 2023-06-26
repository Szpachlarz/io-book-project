using io_book_project.Models;
using Microsoft.EntityFrameworkCore;

namespace io_book_project.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(string id);
        Task<User> GetUserByName(string name);
        Task<IEnumerable<Book>> GetAllFavourites(string id);
        Task<IEnumerable<Book>> GetAFewFavourites(string id);
        Task<IEnumerable<User>> GetAllForThisBook(int bookId);
        bool AddFavourite(UserFavourite favourite);
        bool RemoveFavourite(string userId, int bookId);
        bool CheckIfItIsAlreadyFavourite(string userId, int bookId);
        Task<int> GetCountAsync();
        bool Add(User user);
        bool Update(User user);
        bool Delete(User user);
        bool Save();
        bool UserBan(string userId);
        bool UserUnban(string userId);
    }
}
