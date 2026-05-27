namespace WFMS_WorkForceManagementSystem_WPF_App.Models
{
    public class DepartmentSummary
    {
        public string DepartmentName { get; set; }
        public float TotalHours { get; set; }
        public float AverageHours { get; set; }
        public string TopEmployeeName { get; set; }
        public float TopEmployeeHours { get; set; }
    }
}