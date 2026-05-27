namespace WFMS_WorkForceManagementSystem_WPF_App.Models
{
    public class Employee
    {
        public string Name { get; }
        public string Department { get; }
        public WeekWorkedHours HoursRecord { get; }

        public Employee(string name, string department, WeekWorkedHours hoursRecord)
        {
            Name = name;
            Department = department;
            HoursRecord = hoursRecord;
        }
    }
}