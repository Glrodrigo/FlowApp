using FlowApp.Domain;
using FlowApp.Repository;

namespace FlowApp.Service
{
    public class GoalService : IGoalService
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IUserRepository _userRepository;

        public GoalService(IGoalRepository goalRepository, IUserRepository userRepository)
        {
            _goalRepository = goalRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> CreateAsync(GoalDomain goal)
        {
            try
            {
                List<UserBase> users = new List<UserBase>();
                users = await _userRepository.FindByPrefixAsync(goal.UserPrefix);

                if (users.Count == 0)
                    throw new ArgumentException("Usuário não encontrado em nossa base");

                var goalBase = new GoalBase(users[0].Id, goal.Title, goal.Description, goal.TargetDate);
                var result = await _goalRepository.InsertAsync(goalBase);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<GoalResult>> GetAsync()
        {
            try
            {
                List<GoalResult> result = new List<GoalResult>();
                var goals = await _goalRepository.GetAsync();

                foreach (var goal in goals)
                {
                    GoalResult resultGoal = new GoalResult()
                    {
                        Title = goal.Title,
                        Description = goal.Description,
                        Href = goal.Hash.Substring(0, 12)
                    };

                    result.Add(resultGoal);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> ChangeAsync(GoalParams goalParams)
        {
            try
            {
                GoalParams goal = new GoalParams(goalParams.Title, goalParams.Description, goalParams.GoalPrefix);

                var oldGoals = await _goalRepository.FindByPrefixAsync(goal.GoalPrefix);

                if (oldGoals.Count == 0)
                    throw new ArgumentException("Objetivo não encontrado em nossa base");

                var oldGoal = oldGoals[0];
                oldGoal.SetChangeParams(oldGoal, goal);

                var result = await _goalRepository.UpdateAsync(oldGoal);

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
                var goals = await _goalRepository.FindByPrefixAsync(prefix);

                if (goals.Count == 0)
                    return false;

                var goal = goals[0];    

                await _goalRepository.DeleteAsync(goal);

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
