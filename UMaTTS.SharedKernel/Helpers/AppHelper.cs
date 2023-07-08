namespace UMaTLMS.SharedKernel.Helpers
{
    public static class AppHelper
    {
        public static DayOfWeek? GetDayOfWeek(int day)
        {
            return day switch
            {
                0 => DayOfWeek.Monday,
                1 => DayOfWeek.Tuesday,
                2 => DayOfWeek.Wednesday,
                3 => DayOfWeek.Thursday,
                4 => DayOfWeek.Friday,
                _ => null
            };
        }
    }
}