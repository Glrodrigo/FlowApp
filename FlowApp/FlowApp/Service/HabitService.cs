using FlowApp.Domain;
using FlowApp.Repository;

namespace FlowApp.Service
{
    public class HabitService : IHabitService
    {
        private readonly IHabitRepository _habitRepository;
        private readonly IGoalRepository _goalRepository;

        public HabitService(IHabitRepository habitRepository, IGoalRepository goalRepository)
        {
            _habitRepository = habitRepository;
            _goalRepository = goalRepository;
        }

        public async Task<bool> CreateAsync(HabitDomain habit)
        {
            try
            {
                List<GoalBase> goals = new List<GoalBase>();
                goals = await _goalRepository.FindByPrefixAsync(habit.GoalPrefix);

                if (goals.Count == 0)
                    throw new ArgumentException("Objetivo não encontrado em nossa base");

                var habitBase = new HabitBase(goals[0].Id, habit.Name, habit.FrequencyCode);
                var result = await _habitRepository.InsertAsync(habitBase);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<HabitResult>> GetAsync()
        {
            try
            {
                List<HabitResult> result = new List<HabitResult>();
                var habits = await _habitRepository.GetAsync();

                foreach (var habit in habits)
                {
                    HabitResult resultHabit = new HabitResult()
                    {
                        Name = habit.Name,
                        FrequencyName = habit.FrequencyName,
                        Href = habit.Hash.Substring(0, 12)
                    };

                    result.Add(resultHabit);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> ChangeAsync(HabitParams habitParams)
        {
            try
            {
                HabitParams habit = new HabitParams(habitParams.Name, habitParams.FrequencyCode, habitParams.HabitPrefix);

                var oldHabits = await _habitRepository.FindByPrefixAsync(habit.HabitPrefix);

                if (oldHabits.Count == 0)
                    throw new ArgumentException("Hábito não encontrado em nossa base");

                var oldHabit = oldHabits[0];
                oldHabit.SetChangeParams(oldHabit, habit);

                var result = await _habitRepository.UpdateAsync(oldHabit);

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
                var habits = await _habitRepository.FindByPrefixAsync(prefix);

                if (habits.Count == 0)
                    return false;

                var habit = habits[0];

                await _habitRepository.DeleteAsync(habit);

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
