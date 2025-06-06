using System.Security.Cryptography;
using System.Text;

namespace FlowApp.Shared
{
    public static class HashGenerator
    {
        public static string GenerateHash(Guid id, DateTime createDate)
        {
            string input = $"{id}{createDate:O}"; // Formatar ISO 8601

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                string hash = Convert.ToBase64String(hashBytes)
                    .Replace("+", "-")
                    .Replace("/", "_")
                    .Replace("=", "");

                return hash;
            }
        }
    }
}
