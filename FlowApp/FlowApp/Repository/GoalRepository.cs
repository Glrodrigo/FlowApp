using FlowApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace FlowApp.Repository
{
    public class GoalRepository : IGoalRepository
    {
        private readonly AppDbContext _context;

        public GoalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertAsync(GoalBase goal)
        {
            await _context.Goals.AddAsync(goal);
            int rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }

        public async Task<List<GoalBase>> GetAsync()
        {
            return await _context.Goals.AsNoTracking().ToListAsync() ?? new List<GoalBase>();
        }

        public async Task<List<GoalBase>> FindByPrefixAsync(string prefix)
        {
            var goals = await _context.Goals
                .Where(t => t.Hash.StartsWith(prefix))
                .ToListAsync();

            return goals;
        }

        public async Task<bool> UpdateAsync(GoalBase goal)
        {
            _context.Goals.Update(goal);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(GoalBase goal)
        {
            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();
        }
    }
}
