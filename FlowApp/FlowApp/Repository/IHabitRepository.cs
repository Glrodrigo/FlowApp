using FlowApp.Domain;

namespace FlowApp.Repository
{
    public interface IHabitRepository
    {
        Task<bool> InsertAsync(HabitBase habit);
        Task<List<HabitBase>> GetAsync();
        Task<List<HabitBase>> FindByPrefixAsync(string prefix);
        Task<bool> UpdateAsync(HabitBase habit);
        Task DeleteAsync(HabitBase habit);
    }
}
