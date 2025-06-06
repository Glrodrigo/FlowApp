using FlowApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace FlowApp.Repository
{
    public class HabitRepository : IHabitRepository
    {
        private readonly AppDbContext _context;

        public HabitRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertAsync(HabitBase habit)
        {
            await _context.Habits.AddAsync(habit);
            int rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }

        public async Task<List<HabitBase>> GetAsync()
        {
            return await _context.Habits.AsNoTracking().ToListAsync() ?? new List<HabitBase>();
        }

        public async Task<List<HabitBase>> GetPaginatedAsync(int offset = 0, int limit = 10)
        {
            return await _context.Habits
                .AsNoTracking()
                .OrderBy(p => p.CreateDate)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<List<HabitBase>> FindByPrefixAsync(string prefix)
        {
            var habits = await _context.Habits
                .Where(t => t.Hash.StartsWith(prefix))
                .ToListAsync();

            return habits;
        }

        public async Task<bool> UpdateAsync(HabitBase habit)
        {
            _context.Habits.Update(habit);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(HabitBase habit)
        {
            _context.Habits.Remove(habit);
            await _context.SaveChangesAsync();
        }
    }
}
