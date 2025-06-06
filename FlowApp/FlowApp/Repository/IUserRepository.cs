using FlowApp.Domain;

namespace FlowApp.Repository
{
    public interface IUserRepository
    {
        Task<bool> InsertAsync(UserBase user);
        Task<List<UserBase>> GetAsync();
        Task<List<UserBase>> FindByPrefixAsync(string prefix);
        Task DeleteAsync(UserBase user);
    }
}
