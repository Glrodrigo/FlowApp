using FlowApp.Shared;

namespace FlowApp.Domain
{
    public class GoalParams
    {
        public string GoalPrefix { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public GoalParams() 
        { 

        }

        public GoalParams(string title, string description, string goalPrefix)
        {
            if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(description))
                throw new ArgumentException("Campos não foram preenchidos");

            if (!string.IsNullOrEmpty(title))
            {
                if (title.Length > 100)
                    throw new ArgumentException("Nome muito extenso");
            }

            if (!string.IsNullOrEmpty(description))
            {
                if (description.Length > 300)
                    throw new ArgumentException("Descrição muito extenso");
            }

            this.Title = !string.IsNullOrWhiteSpace(title) ? TextUtils.NormalizeField(title).ToUpper() : null;
            this.Description = !string.IsNullOrWhiteSpace(description) ? TextUtils.NormalizeField(description) : null;
            this.GoalPrefix = goalPrefix;
        }
    }
}
