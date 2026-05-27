using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace WFMS_WorkForceManagementSystem_WPF_App.Views
{
    /// <summary>
    /// Interaction logic for FileLoadPanel.xaml
    /// </summary>
    public partial class FileLoadPanel : UserControl
    {
        // An event that notifies our Controller when a file path is chosen
        public event Action<string> FileSelected;

        public FileLoadPanel()
        {
            InitializeComponent();
        }

        // Method called by the Controller to change the error text on screen
        public void ShowError(string message)
        {
            TxtStatus.Text = message;
            TxtStatus.Visibility = Visibility.Visible;
        }

        // Event handler bound to the Browse button click in XAML
        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text & CSV Files (*.txt;*.csv)|*.txt;*.csv|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Trigger our event and send the file path over to the Controller
                FileSelected?.Invoke(openFileDialog.FileName);
            }
        }
    }
}
