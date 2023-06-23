using io_book_project.Models;

namespace io_book_project.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(string id);
        Task<IEnumerable<Book>> GetAllFavourites(string id);
        Task<IEnumerable<Book>> GetAFewFavourites(string id);
        bool AddFavourite(UserFavourite favourite);
        bool Add(User user);
        bool Update(User user);
        bool Delete(User user);
        bool Save();
    }
}
