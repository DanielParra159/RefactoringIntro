using System.Globalization;

namespace Code
{
    public static class FormatUtilities
    {
        public static string Usd(float aNumber)
        {
            return (aNumber / 100f).ToString("C3", CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}