using FlowApp.Shared;

namespace FlowApp.Domain
{
    public class HabitParams
    {
        public string HabitPrefix { get; set; }
        public string? Name { get; set; }
        public int? FrequencyCode { get; set; }


        public HabitParams() 
        { 

        }

        public HabitParams(string? name, int? frequencyCode, string habitPrefix)
        {
            if (string.IsNullOrEmpty(name) && frequencyCode == null)
                throw new ArgumentException("Campos não foram preenchidos");

            if (!string.IsNullOrEmpty(name))
            {
                if (name.Length > 100)
                    throw new ArgumentException("Nome muito extenso");
            }

            if (frequencyCode != null)
            {
                if (frequencyCode < 0 || frequencyCode > 2)
                    throw new ArgumentException("Código inválido ou vazio");
            }

            this.Name = !string.IsNullOrWhiteSpace(name) ? TextUtils.NormalizeField(name).ToUpper() : null;
            this.FrequencyCode = frequencyCode != null ? frequencyCode : null;
            this.HabitPrefix = habitPrefix;
        }
    }
}
