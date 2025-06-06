using FlowApp.Domain;

namespace FlowApp.Service
{
    public interface IHabitService
    {
        Task<bool> CreateAsync(HabitDomain habit);
        Task<List<HabitResult>> GetAsync();
        Task<bool> ChangeAsync(HabitParams habitParams);
        Task<bool> DeleteAsync(string prefix);
    }
}
