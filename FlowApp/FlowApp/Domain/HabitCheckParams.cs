using FlowApp.Shared;
using System.Xml.Linq;

namespace FlowApp.Domain
{
    public class HabitCheckParams
    {
        public string HabitCheckPrefix { get; set; }
        public bool Completed { get; set; }
        public DateTime? CompletedDate { get; set; }

        public HabitCheckParams() 
        { 

        }

        public HabitCheckParams(bool completed, string habitCheckPrefix, DateTime? completedDate)
        {
            if (completed && completedDate == null)
                completedDate = DateTime.UtcNow;

            this.Completed = completed;
            this.CompletedDate = completedDate;
            this.HabitCheckPrefix = habitCheckPrefix;
        }
    }
}
