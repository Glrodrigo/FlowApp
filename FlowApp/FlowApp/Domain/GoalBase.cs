using FlowApp.Shared;

namespace FlowApp.Domain
{
    public class GoalBase
    {
        public Guid Id { get; set; }
        public Guid UserId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime TargetDate { get; private set; }
        public string Hash { get; private set; }
        public DateTime CreateDate { get; private set; }

        public UserBase User { get; set; } // Relacionar a este

        public GoalBase() 
        { 

        }

        public GoalBase(Guid userId, string title, string description, DateTime targetDate)
        {
            ValidateTitle(title);
            ValidateDescription(description);

            this.UserId = userId;
            this.Title = TextUtils.NormalizeField(title);
            this.Description = TextUtils.NormalizeField(description);
            this.TargetDate = targetDate;
            this.CreateDate = DateTime.UtcNow;
            this.Hash = HashGenerator.GenerateHash(Guid.NewGuid(), CreateDate);
        }

        public void SetDescription(string description)
        {
            ValidateDescription(description);

            this.Description = description;
        }

        public void SetChangeParams(GoalBase oldGoal, GoalParams goalParams)
        {
            if (!string.IsNullOrEmpty(goalParams.Title))
            {
                if (oldGoal.Title != goalParams.Title)
                    oldGoal.Title = goalParams.Title;
            }

            if (!string.IsNullOrEmpty(goalParams.Description))
            {
                if (oldGoal.Description != goalParams.Description)
                    oldGoal.Description = goalParams.Description;
            }
        }

        private void ValidateTitle(string title)
        {
            if (string.IsNullOrEmpty(title) || title == "string")
                throw new ArgumentException("Nome inválido ou vazio");

            if (title.Length > 100)
                throw new ArgumentException("Nome muito extenso");
        }

        private void ValidateDescription(string description)
        {
            if (string.IsNullOrEmpty(description) || description == "string")
                throw new ArgumentException("Descrição inválido ou vazio");

            if (description.Length > 300)
                throw new ArgumentException("Descrição muito extensa");
        }
    }
}
