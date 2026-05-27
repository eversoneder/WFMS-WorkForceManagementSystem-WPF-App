using System.Linq;

namespace WFMS_WorkForceManagementSystem_WPF_App.Models
{
    public class WeekWorkedHours
    {
        private readonly float[] _hours = new float[5];

        public void SetDayHour(int index, float hourValue)
        {
            if (index >= 0 && index < 5)
            {
                _hours[index] = hourValue;
            }
        }

        public float[] GetHoursArray() => _hours;

        // Automatically sums up the array without manual loops
        public float TotalWeeklyHours => _hours.Sum();

        public float[] HoursList => GetHoursArray();
    }
}