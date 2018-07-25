namespace FluentBuilder.Extensions
{
    public static class StringExtensions
    {
        public static string RemovePrefix(this string sourceString, string prefix)
        {
            if (sourceString.IndexOf(prefix) == 0)
                return sourceString.Remove(0, prefix.Length);

            return sourceString;
        }
    }
}
