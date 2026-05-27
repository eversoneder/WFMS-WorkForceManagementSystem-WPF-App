using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using WFMS_WorkForceManagementSystem_WPF_App.Models;
using WFMS_WorkForceManagementSystem_WPF_App.Views;

namespace WFMS_WorkForceManagementSystem_WPF_App.Controllers
{
    public class WfmsController
    {
        private readonly MainWindow _mainWindow;
        private readonly FileService _fileService;
        private readonly WfmsEngine _wfmsEngine;

        private const string DefaultPath = "TextFiles/SD-TA-001-A_OrganisationWeeklyTimesheet.csv";

        private List<Employee> _loadedEmployees = new List<Employee>();
        private List<FilterDepartment> _departmentFilters = new List<FilterDepartment>();
        private FileLoadPanel? _loadPanel;
        private DataDashboardPanel? _dashboardPanel;

        public WfmsController(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _fileService = new FileService();
            _wfmsEngine = new WfmsEngine();
        }

        public void StartApplication()
        {
            ShowLoadScreen();
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DefaultPath);

            if (File.Exists(fullPath))
                ProcessIncomingFile(fullPath);
            else
                _loadPanel.ShowError("Default file path missing. Use browse button below.");
        }

        private void ShowLoadScreen()
        {
            _loadPanel = new FileLoadPanel();
            _loadPanel.FileSelected += ProcessIncomingFile;
            _mainWindow.MainContainer.Children.Clear();
            _mainWindow.MainContainer.Children.Add(_loadPanel);
        }

        private void ProcessIncomingFile(string filePath)
        {
            try
            {
                _loadedEmployees = _fileService.ReadEmployeeFile(filePath);
                var uniqueDepts = _wfmsEngine.GetUniqueDepartments(_loadedEmployees);
                _departmentFilters = uniqueDepts.Select(d => new FilterDepartment { Name = d, IsChecked = true }).ToList();
                ShowDashboardScreen();
            }
            catch (Exception ex)
            {
                _loadPanel?.ShowError($"Load Failure: {ex.Message}");
            }
        }

        private void ShowDashboardScreen()
        {
            _dashboardPanel = new DataDashboardPanel();
            _dashboardPanel.PopulateDepartments(_departmentFilters);

            // NEW LINE: Feed the complete raw employee list directly into the new reference grid
            _dashboardPanel.PopulateRawDataGrid(_loadedEmployees);

            _dashboardPanel.FiltersChanged += RefreshCalculations;
            _dashboardPanel.ExportRequested += HandleCustomExport;
            _dashboardPanel.ExitRequested += ShowLoadScreen;

            _mainWindow.MainContainer.Children.Clear();
            _mainWindow.MainContainer.Children.Add(_dashboardPanel);
            RefreshCalculations();
        }

        private void RefreshCalculations()
        {
            var activeDepts = _departmentFilters.Where(f => f.IsChecked).Select(f => f.Name).ToList();
            var summaries = _wfmsEngine.ProcessSelectedData(_loadedEmployees, activeDepts);

            var uiProjection = summaries.Select(s => new
            {
                Department = s.DepartmentName,
                AverageHours = _dashboardPanel.IsAvgChecked ? s.AverageHours.ToString("N2") : "---",
                TotalHours = _dashboardPanel.IsTotalChecked ? s.TotalHours.ToString("N2") : "---",
                TopEmployee = _dashboardPanel.IsMostHoursChecked ? s.TopEmployeeName : "---",
                TopEmployeeHours = _dashboardPanel.IsMostHoursChecked ? s.TopEmployeeHours.ToString("N2") : "---"
            }).ToList();

            _dashboardPanel.UpdateGrid(uiProjection);
        }

        private void HandleCustomExport()
        {
            var activeDepts = _departmentFilters.Where(f => f.IsChecked).Select(f => f.Name).ToList();
            var currentSummaries = _wfmsEngine.ProcessSelectedData(_loadedEmployees, activeDepts);

            if (!currentSummaries.Any())
            {
                MessageBox.Show("Please check at least one department first.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "Text Files (*.txt)|*.txt", FileName = "CustomTotals.txt" };
            if (saveFileDialog.ShowDialog() == true)
            {
                _wfmsEngine.ExportCustomReport(saveFileDialog.FileName, currentSummaries, _dashboardPanel.IsAvgChecked, _dashboardPanel.IsTotalChecked, _dashboardPanel.IsMostHoursChecked);
                MessageBox.Show("Custom file saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
