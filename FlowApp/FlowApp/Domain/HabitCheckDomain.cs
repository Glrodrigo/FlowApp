namespace FlowApp.Domain
{
    public class HabitCheckDomain
    {
        public string HabitPrefix { get; set; }
        public bool Completed { get; set; }
        public DateTime? CompletedDate { get; set; }

        public HabitCheckDomain() 
        {
            this.Completed = false;
        }
    }
}
