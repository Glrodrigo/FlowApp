using FlowApp.Shared;

namespace FlowApp.Domain
{
    public class HabitBase
    {
        public Guid Id { get; set; }
        public Guid GoalId { get; set; }
        public string Name { get; private set; }
        public int FrequencyCode { get; private set; }
        public string FrequencyName { get; private set; }
        public string Hash {  get; private set; }
        public DateTime CreateDate { get; private set; }

        public GoalBase Goal { get; set; } // Relacionar a este

        public HabitBase()
        {

        }

        public HabitBase(Guid goalId, string name, int frequencyCode) 
        {
            ValidateName(name);
            ValidateFrequencyCode(frequencyCode);
            ChangeFrequencyName(frequencyCode);

            this.GoalId = goalId;
            this.Name = TextUtils.NormalizeField(name);
            this.CreateDate = DateTime.UtcNow;
            this.Hash = HashGenerator.GenerateHash(Guid.NewGuid(), CreateDate);
        }

        public void SetChangeParams(HabitBase oldHabit, HabitParams habitParams)
        {
            if (!string.IsNullOrEmpty(habitParams.Name))
            {
                if (oldHabit.Name != habitParams.Name)
                    oldHabit.Name = habitParams.Name;
            }

            if (habitParams.FrequencyCode != null)
                ChangeFrequencyName((int)habitParams.FrequencyCode);
        }

        private void ChangeFrequencyName(int frequencyCode)
        {
            if (frequencyCode == 1)
                this.FrequencyName = "SomeTimes";
            else if (frequencyCode == 2)
                this.FrequencyName = "Weekly";
            else
                this.FrequencyName = "Daily";
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name) || name == "string")
                throw new ArgumentException("Nome inválido ou vazio");

            if (name.Length > 100)
                throw new ArgumentException("Nome muito extenso");
        }

        private void ValidateFrequencyCode(int code)
        {
            if (code < 0 || code > 2)
                throw new ArgumentException("Código inválido ou vazio");
        }
    }
}
