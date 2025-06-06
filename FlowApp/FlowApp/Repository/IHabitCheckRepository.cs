using FlowApp.Domain;

namespace FlowApp.Repository
{
    public interface IHabitCheckRepository
    {
        Task<bool> InsertAsync(HabitCheckBase habitCheck);
        Task<List<HabitCheckBase>> GetAsync();
        Task<List<HabitCheckBase>> GetPaginatedAsync(int offset, int limit);
        Task<List<HabitCheckBase>> FindByPrefixAsync(string prefix);
        Task<bool> UpdateAsync(HabitCheckBase habitCheck);
    }
}
