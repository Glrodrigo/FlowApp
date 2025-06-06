using FlowApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace FlowApp.Repository
{
    public class HabitCheckRepository : IHabitCheckRepository
    {
        private readonly AppDbContext _context;

        public HabitCheckRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertAsync(HabitCheckBase habitCheck)
        {
            await _context.HabitChecks.AddAsync(habitCheck);
            int rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }

        public async Task<List<HabitCheckBase>> GetAsync()
        {
            return await _context.HabitChecks.AsNoTracking().ToListAsync() ?? new List<HabitCheckBase>();
        }

        public async Task<List<HabitCheckBase>> GetPaginatedAsync(int offset = 0, int limit = 10)
        {
            return await _context.HabitChecks
                .AsNoTracking()
                .OrderBy(p => p.CompletedDate)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<List<HabitCheckBase>> FindByPrefixAsync(string prefix)
        {
            var habits = await _context.HabitChecks
                .Where(t => t.Hash.StartsWith(prefix))
                .ToListAsync();

            return habits;
        }

        public async Task<bool> UpdateAsync(HabitCheckBase habitCheck)
        {
            _context.HabitChecks.Update(habitCheck);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
