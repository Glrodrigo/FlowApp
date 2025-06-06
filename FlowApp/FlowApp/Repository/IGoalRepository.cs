using FlowApp.Domain;

namespace FlowApp.Repository
{
    public interface IGoalRepository
    {
        Task<bool> InsertAsync(GoalBase goal);
        Task<List<GoalBase>> GetAsync();
        Task<List<GoalBase>> FindByPrefixAsync(string prefix);
        Task<bool> UpdateAsync(GoalBase goal);
        Task DeleteAsync(GoalBase goal);
    }
}
