using FlowApp.Domain;

namespace FlowApp.Service
{
    public interface IUserService
    {
        Task<bool> CreateAsync(UserDomain user);
        Task<List<UserResult>> GetAsync();
        Task<bool> DeleteAsync(string prefix);
    }
}
