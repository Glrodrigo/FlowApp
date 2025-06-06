using FlowApp.Domain;

namespace FlowApp.Service
{
    public interface IGoalService
    {
        Task<bool> CreateAsync(GoalDomain goal);
        Task<List<GoalResult>> GetAsync();
        Task<bool> ChangeAsync(GoalParams goalParams);
        Task<bool> DeleteAsync(string prefix);
    }
}
