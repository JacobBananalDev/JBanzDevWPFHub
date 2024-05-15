using System.Runtime.CompilerServices;

namespace JBanzDevCommonTools.ExtensionMethods
{
    public static class JbanzDevExtensions
    {
        public static bool IsNotEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotNull<T>(this T obj) where T : class
        {
            return obj != null;
        }
    } 
}
