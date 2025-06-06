using FlowApp.Domain;

namespace FlowApp.Service
{
    public interface IHabitCheckService
    {
        Task<bool> CreateAsync(HabitCheckDomain habitCheck);
        Task<List<HabitCheckResult>> GetAsync();
        Task<bool> ChangeAsync(HabitCheckParams habitCheckParams);
    }
}
