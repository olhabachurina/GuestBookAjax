using GuestBookAjax.Models;

namespace GuestBookAjax.Data
{
    public interface IRepository
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByNameAsync(string name);
        Task<List<Message>> GetAllMessagesAsync();
        Task<List<Message>> GetMessagesByUserIdAsync(int userId);
        Task AddMessageAsync(Message message);
        Task AddUserAsync(User user);
    }
}