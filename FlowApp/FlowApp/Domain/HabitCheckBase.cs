using FlowApp.Shared;

namespace FlowApp.Domain
{
    public class HabitCheckBase
    {
        public Guid Id { get; set; }
        public Guid HabitId { get; set; }
        public bool Completed { get; private set; }
        public string Hash { get; private set; }
        public string HabitHash { get; private set; }
        public DateTime? CompletedDate { get; private set; }

        public HabitBase Habit { get; set; } // Relacionar a este

        public HabitCheckBase() 
        { 

        }

        public HabitCheckBase(Guid habitId, string habitHash, bool completed, DateTime? completedDate)
        {
            this.HabitId = habitId;
            this.Completed = completed;
            this.CompletedDate = completedDate;
            this.HabitHash = habitHash;
            this.Hash = HashGenerator.GenerateHash(Guid.NewGuid(), DateTime.UtcNow);
        }

        public void SetChangeParams(HabitCheckBase oldHabitCheck, HabitCheckParams habitCheckParams)
        {
            if (habitCheckParams.Completed)
            {
                if (oldHabitCheck.Completed != habitCheckParams.Completed)
                    oldHabitCheck.Completed = habitCheckParams.Completed;
            }

            if (habitCheckParams.CompletedDate != null)
            {
                if (oldHabitCheck.CompletedDate != habitCheckParams.CompletedDate)
                    oldHabitCheck.CompletedDate = habitCheckParams.CompletedDate;
            }
        }
    }
}
