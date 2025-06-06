using FlowApp.Domain;
using FlowApp.Repository;

namespace FlowApp.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateAsync(UserDomain user)
        {
            try
            {
                var userBase = new UserBase(user.Name, user.Email, user.Password);
                var result = await _userRepository.InsertAsync(userBase);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UserResult>> GetAsync()
        {
            try
            {
                List<UserResult> result = new List<UserResult>();
                var users = await _userRepository.GetAsync();

                foreach (var user in users)
                {
                    UserResult resultUser = new UserResult()
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Href = user.Hash.Substring(0, 12)
                    };

                    result.Add(resultUser);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string prefix)
        {
            try
            {
                var users = await _userRepository.FindByPrefixAsync(prefix);

                if (users.Count == 0)
                    return false;

                var user = users[0];

                await _userRepository.DeleteAsync(user);

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
