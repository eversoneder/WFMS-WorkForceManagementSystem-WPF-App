using System.Configuration;
using System.Data;
using System.Windows;
using WFMS_WorkForceManagementSystem_WPF_App.Controllers;

namespace WFMS_WorkForceManagementSystem_WPF_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow mainWindow = new MainWindow();
            WfmsController controller = new WfmsController(mainWindow);

            mainWindow.Show();
            controller.StartApplication();
        }
    }

}
