using System.Windows;
using TaskKillerAdministrator.MVVM.ViewModels;
using TaskKillerDLL;

namespace TaskKillerAdministrator
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindowViewModel mainWindowViewModel;
        private void Application_Startup(object sender, StartupEventArgs e)
        {            
            var mainWindow = new MainWindow();
            mainWindowViewModel = new MainWindowViewModel();
            MainWindow.DataContext = mainWindowViewModel;
            MainWindow.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            FileHelper.SaveComputersList(mainWindowViewModel.computersViewModel.ComputerList);
        }
    }
}
