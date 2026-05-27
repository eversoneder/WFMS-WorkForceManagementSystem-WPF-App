using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WFMS_WorkForceManagementSystem_WPF_App.Models;
namespace WFMS_WorkForceManagementSystem_WPF_App.Views
{
    /// <summary>
    /// Interaction logic for DataDashboardPanel.xaml
    /// </summary>
    public partial class DataDashboardPanel : UserControl
    {
        public event Action FiltersChanged;
        public event Action ExportRequested;
        public event Action ExitRequested;

        public DataDashboardPanel()
        {
            InitializeComponent();
        }

        // Method called by the controller to populate the dynamic department checklist
        public void PopulateDepartments(List<FilterDepartment> depts)
        {
            LstDepartments.ItemsSource = depts;
        }

        // Method called by the controller to refresh rows inside the DataGrid
        public void UpdateGrid(System.Collections.IEnumerable data)
        {
            DgridDisplay.ItemsSource = null; // Clear out the old binding first to ensure it fully refreshes
            DgridDisplay.ItemsSource = data; // Assign the new filtered rows smoothly
        }

        /// <summary>
        /// Method called by the controller to populate the static raw text data from the file
        /// </summary>
        /// <param name="rawEmployees">employee list to populate '3. File Data Display:' grid</param>
        public void PopulateRawDataGrid(List<WFMS_WorkForceManagementSystem_WPF_App.Models.Employee> rawEmployees)
        {
            DgridRawData.ItemsSource = null;
            DgridRawData.ItemsSource = rawEmployees;
        }

        // Safe boolean helper properties to check checkbox state
        public bool IsAvgChecked => ChkAvg.IsChecked == true;
        public bool IsTotalChecked => ChkTotal.IsChecked == true;
        public bool IsMostHoursChecked => ChkMost.IsChecked == true;

        private void FilterChanged(object sender, RoutedEventArgs e)
        {
            FiltersChanged?.Invoke();
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            ExportRequested?.Invoke();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ExitRequested?.Invoke();
        }
    }
}
