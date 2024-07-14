using GuestBookAjax.Models;
using Microsoft.EntityFrameworkCore;

namespace GuestBookAjax.Data
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
        }

        public async Task<List<Message>> GetAllMessagesAsync()
        {
            return await _context.Messages.Include(m => m.User).ToListAsync();
        }

        public async Task<List<Message>> GetMessagesByUserIdAsync(int userId)
        {
            return await _context.Messages.Where(m => m.Id_User == userId).Include(m => m.User).ToListAsync();
        }

        public async Task AddMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
