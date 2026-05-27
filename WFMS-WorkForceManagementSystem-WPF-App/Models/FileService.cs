using System;
using System.Collections.Generic;
using System.IO;

namespace WFMS_WorkForceManagementSystem_WPF_App.Models
{
    public class FileService
    {
        public List<Employee> ReadEmployeeFile(string filePath)
        {
            var parsedEmployees = new List<Employee>();

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Target file not found at path: {filePath}");
            }

            using (var reader = new StreamReader(filePath))
            {
                string header = reader.ReadLine(); // Consume header row
                int lineCounter = 1;

                while (!reader.EndOfStream)
                {
                    lineCounter++;
                    string line = reader.ReadLine();

                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] lineParts = line.Split(',');
                    if (lineParts.Length != 7) continue;

                    string name = lineParts[0].Trim();
                    string department = lineParts[1].Trim();
                    var weekHours = new WeekWorkedHours();

                    for (int i = 2; i <= 6; i++)
                    {
                        if (!float.TryParse(lineParts[i].Trim(), out float dailyHour))
                        {
                            throw new FormatException($"Data Error on line {lineCounter}: Column index {i} could not parse float value.");
                        }
                        weekHours.SetDayHour(i - 2, dailyHour);
                    }

                    parsedEmployees.Add(new Employee(name, department, weekHours));
                }
            }
            return parsedEmployees;
        }
    }
}