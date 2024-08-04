namespace DebuggingAndRefactoringTask1.Extensions
{
    internal static class Extensions
    {
        public static string FormatDate(this DateTime dateTime)
        {
            var localTime = dateTime.ToLocalTime();
            return localTime.ToString("dd-MMM-yyyy HH:mm");
        }

        public static bool IsValidAmount(this string? amount)
        {
            if (string.IsNullOrWhiteSpace(amount))
            {
                return false;
            }

            var parsed = decimal.TryParse(amount, out var value);

            if (parsed)
            {
                if (value <= 0)
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public static (bool, string) IsValidName(this string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return (false, "Name cannot be empty");
            }

            if (name.Length > 50)
            {
                return (false, "Name cannot be more than 50 characters");
            }

            if (name.Any(char.IsDigit))
            {
                return (false, "Name cannot contain digits");
            }

            //plus whatever other checks e.g. dodgy characters

            return (true, string.Empty);
        }
    }
}
