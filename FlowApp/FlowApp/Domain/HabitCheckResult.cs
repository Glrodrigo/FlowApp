namespace FlowApp.Domain
{
    public class HabitCheckResult
    {
        public string HabitHref { get; set; }
        public bool Completed { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
