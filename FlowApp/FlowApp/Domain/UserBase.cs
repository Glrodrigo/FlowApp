using FlowApp.Shared;

namespace FlowApp.Domain
{
    public class UserBase
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Hash { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime CreateDate { get; private set; }

        public UserBase() 
        { 

        }

        public UserBase(string name, string email, string password)
        {
            ValidateName(name);
            ValidateEmail(email);

            this.Name = TextUtils.NormalizeField(name);
            this.Email = email;
            this.CreateDate = DateTime.UtcNow;
            this.Hash = HashGenerator.GenerateHash(Guid.NewGuid(), CreateDate);
            this.PasswordHash = PasswordHelper.HashPassword(password);
        }

        public void SetName(string name)
        {
            ValidateName(name);

            this.Name = name.ToUpper();
        }

        public void SetEmail(string email)
        {
            ValidateEmail(email);

            this.Email = email;
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name) || name == "string")
                throw new ArgumentException("Nome inválido ou vazio");

            if (name.Length > 200)
                throw new ArgumentException("Nome muito extenso");
        }

        private void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || email == "string")
                throw new ArgumentException("Email inválido ou vazio");

            if (email.Length > 300)
                throw new ArgumentException("Email muito extenso");

            if (!EmailValidator.IsValid(email))
                throw new ArgumentException("Email está inválido");
        }


        //public bool CheckPassword(string password)
        //{
        //    return PasswordHelper.VerifyPassword(password, PasswordHash);
        //}
    }
}
