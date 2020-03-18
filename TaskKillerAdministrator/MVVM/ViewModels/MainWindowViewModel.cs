using System.Windows;

namespace TaskKillerAdministrator.MVVM.ViewModels
{
    public class MainWindowViewModel : DependencyObject
    {
        public readonly ComputersViewModel computersViewModel = new ComputersViewModel();

        public static string RAMLimitString { get; set; } = "";


        public object SelectedViewModel
        {
            get { return (object)GetValue(SelectedViewModelProperty); }
            set { SetValue(SelectedViewModelProperty, value); }
        }

        public static readonly DependencyProperty SelectedViewModelProperty =
            DependencyProperty.Register(nameof(SelectedViewModel), typeof(object), typeof(MainWindowViewModel), new PropertyMetadata(null));

        public MainWindowViewModel()
        {
            SelectedViewModel = computersViewModel;
        }
    //    public RelayCommand SetRamLimit => new RelayCommand(obj =>
    //    {
    //       userLogin;
    //}, (o) => SelectedComputerInfo != null);
    }
}
