using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WFMS_WorkForceManagementSystem_WPF_App.Models
{
    public class WfmsEngine
    {
        public List<string> GetUniqueDepartments(List<Employee> employees)
        {
            return employees
                .Select(e => e.Department)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(d => d)
                .ToList();
        }

        public List<DepartmentSummary> ProcessSelectedData(List<Employee> allEmployees, List<string> selectedDepartments)
        {
            var summaryList = new List<DepartmentSummary>();

            var filteredEmployees = allEmployees
                .Where(e => selectedDepartments.Contains(e.Department, StringComparer.OrdinalIgnoreCase));

            var groupedByDept = filteredEmployees.GroupBy(e => e.Department, StringComparer.OrdinalIgnoreCase);

            foreach (var group in groupedByDept)
            {
                var topEmployee = group.OrderByDescending(e => e.HoursRecord.TotalWeeklyHours).FirstOrDefault();
                int employeeCount = group.Count();

                var summary = new DepartmentSummary
                {
                    DepartmentName = group.Key,
                    TotalHours = group.Sum(e => e.HoursRecord.TotalWeeklyHours),
                    AverageHours = employeeCount > 0 ? group.Sum(e => e.HoursRecord.TotalWeeklyHours) / employeeCount : 0f,
                    TopEmployeeName = topEmployee != null ? topEmployee.Name : "N/A",
                    TopEmployeeHours = topEmployee != null ? topEmployee.HoursRecord.TotalWeeklyHours : 0f
                };
                summaryList.Add(summary);
            }
            return summaryList;
        }

        public void ExportCustomReport(string saveFilePath, List<DepartmentSummary> summaries, bool showAvg, bool showTotal, bool showMost)
        {
            using (var writer = new StreamWriter(saveFilePath))
            {
                writer.WriteLine("----- Custom Employee Weekly Report -----");
                writer.WriteLine($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n");

                foreach (var summary in summaries)
                {
                    writer.WriteLine($"DEPARTMENT: {summary.DepartmentName.ToUpper()}");
                    if (showAvg) writer.WriteLine($"   Average employee hours worked: {summary.AverageHours:F2} Hours.");
                    if (showTotal) writer.WriteLine($"   Total Hours Worked: {summary.TotalHours:F2} Hours.");
                    if (showMost) writer.WriteLine($"   Employee with Most Hours Worked: {summary.TopEmployeeName} has worked {summary.TopEmployeeHours:F2} Hours.");
                    writer.WriteLine();
                }
            }
        }
    }
}
