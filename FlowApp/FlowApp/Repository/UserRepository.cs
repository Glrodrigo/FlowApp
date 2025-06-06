using FlowApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace FlowApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<bool> InsertAsync(UserBase user)
        {
            await _context.Users.AddAsync(user);
            int rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }

        public async Task<List<UserBase>> GetAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync() ?? new List<UserBase>();
        }

        public async Task<List<UserBase>> FindByPrefixAsync(string prefix)
        {
            var users = await _context.Users
                .Where(t => t.Hash.StartsWith(prefix))
                .ToListAsync();

            return users;
        }

        public async Task DeleteAsync(UserBase user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
