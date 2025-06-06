using FlowApp.Domain;
using FlowApp.Repository;

namespace FlowApp.Service
{
    public class HabitCheckService : IHabitCheckService
    {
        private readonly IHabitCheckRepository _habitCheckRepository;
        private readonly IHabitRepository _habitRepository;

        public HabitCheckService(IHabitCheckRepository habitCheckRepository, IHabitRepository habitRepository)
        {
            _habitCheckRepository = habitCheckRepository;
            _habitRepository = habitRepository;
        }

        public async Task<bool> CreateAsync(HabitCheckDomain habitCheck)
        {
            try
            {
                List<HabitBase> habits = new List<HabitBase>();
                habits = await _habitRepository.FindByPrefixAsync(habitCheck.HabitPrefix);

                if (habits.Count == 0)
                    throw new ArgumentException("Hábito não encontrado em nossa base");

                var habitCheckBase = new HabitCheckBase(habits[0].Id, habits[0].Hash, habitCheck.Completed, habitCheck.CompletedDate);
                var result = await _habitCheckRepository.InsertAsync(habitCheckBase);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<HabitCheckResult>> GetAsync()
        {
            try
            {
                List<HabitCheckResult> result = new List<HabitCheckResult>();
                var habitChecks = await _habitCheckRepository.GetAsync();

                foreach (var habitCheck in habitChecks)
                {
                    HabitCheckResult resultHabitCheck = new HabitCheckResult()
                    {
                        Completed = habitCheck.Completed,
                        CompletedDate = habitCheck.CompletedDate,
                        HabitHref = habitCheck.HabitHash.Substring(0, 12)
                    };

                    result.Add(resultHabitCheck);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> ChangeAsync(HabitCheckParams habitCheckParams)
        {
            try
            {
                HabitCheckParams habitCheck = new HabitCheckParams(habitCheckParams.Completed, habitCheckParams.HabitCheckPrefix, habitCheckParams.CompletedDate);

                var oldCheckHabits = await _habitCheckRepository.FindByPrefixAsync(habitCheck.HabitCheckPrefix);

                if (oldCheckHabits.Count == 0)
                    throw new ArgumentException("Hábito não encontrado em nossa base");

                var oldCheckHabit = oldCheckHabits[0];
                oldCheckHabit.SetChangeParams(oldCheckHabit, habitCheck);

                var result = await _habitCheckRepository.UpdateAsync(oldCheckHabit);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
