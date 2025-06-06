namespace FlowApp.Shared
{
    public static class TextUtils
    {
        public static string NormalizeField(string name)
        {
            return string.Join(" ",
                name.Trim()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)) // Remover espaços duplicados
                .ToUpper();
        }
    }
}
