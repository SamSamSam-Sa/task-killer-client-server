using System.Collections.ObjectModel;
using System.Windows;
using TaskKillerDLL;

namespace TaskKillerAdministrator.MVVM.ViewModels
{
    public class ComputersViewModel: DependencyObject 
    {

        public ObservableCollection<ComputerInfo> ComputerList { get; set; } = new ObservableCollection<ComputerInfo>();

        public ComputerInfo SelectedComputerInfo
        {
            get { return (ComputerInfo)GetValue(SelectedComputerInfoProperty); }
            set { SetValue(SelectedComputerInfoProperty, value); }
        }
        public static readonly DependencyProperty SelectedComputerInfoProperty =
            DependencyProperty.Register(nameof(SelectedComputerInfo), typeof(ComputerInfo), typeof(MainWindowViewModel), new PropertyMetadata(null));

        public RelayCommand AddComputerCommand => new RelayCommand(obj =>
        {
            ComputerInfo computerInfo = new ComputerInfo("Number", "Login", "IP", 0);
            ComputerList.Add(computerInfo);
            SelectedComputerInfo = computerInfo;
        });
        public RelayCommand RemoveComputerCommand => new RelayCommand(obj =>
        {
            ComputerList.Remove(SelectedComputerInfo);
        }, (o) => SelectedComputerInfo != null);

        public RelayCommand OpenProcessesCommand => new RelayCommand(obj =>
        {
            var mainWindowViewModel = (MainWindowViewModel)App.Current.MainWindow.DataContext;
            FileHelper.SaveComputersList(mainWindowViewModel.computersViewModel.ComputerList);
            mainWindowViewModel.SelectedViewModel = new ProcessesViewModel(SelectedComputerInfo.IP, SelectedComputerInfo.RAM);
        }, (o) => (SelectedComputerInfo != null)&&(MainWindowViewModel.RAMLimitString!=""));

        public ComputersViewModel()
        {
            if (FileHelper.TryGetComputersList(out var computersList))
            {
                ComputerList = new ObservableCollection<ComputerInfo>(computersList);
            }
        }

    }
}
