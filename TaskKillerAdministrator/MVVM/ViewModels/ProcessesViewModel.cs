using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using TaskKillerDLL;

namespace TaskKillerAdministrator.MVVM.ViewModels
{
    public class ProcessesViewModel : DependencyObject
    {
        private float ComputerRAM { get; set; } = 0;

        private string ComputerIP;

        public string PATH;
        public ObservableCollection<TaskInfo> TaskList { get; set; } = new ObservableCollection<TaskInfo>();
        public TaskInfo SelectedTaskInfo
        {
            get { return (TaskInfo)GetValue(SelectedTaskInfoProperty); }
            set { SetValue(SelectedTaskInfoProperty, value); }
        }
        public static readonly DependencyProperty SelectedTaskInfoProperty =
            DependencyProperty.Register(nameof(SelectedTaskInfo), typeof(TaskInfo), typeof(MainWindowViewModel), new PropertyMetadata(null));


        public RelayCommand OpenComputersCommand => new RelayCommand(obj =>
        {
            var mainViewModel = (MainWindowViewModel)App.Current.MainWindow.DataContext;
            mainViewModel.SelectedViewModel = mainViewModel.computersViewModel;
        });

        public RelayCommand KillTaskCommand => new RelayCommand(obj =>
        {
            var isProcessKilled = WebService.AskToManageTask($"{PATH}/killTask", SelectedTaskInfo);
            if (isProcessKilled)
            {
                MessageBox.Show("Killed");
                RefresfProcessesList();
            }                  
            else
            {
                MessageBox.Show("Didn't kill");
            }
        }, (o) => SelectedTaskInfo != null);

        public RelayCommand RestartTaskCommand => new RelayCommand(obj =>
        {
            var isProcessRestarted = WebService.AskToManageTask($"{PATH}/restartTask", SelectedTaskInfo);
            if (isProcessRestarted)
            {
                MessageBox.Show("Restarted");
            }
            else
            {
                MessageBox.Show("Didn't restart");
            }
        }, (o) => SelectedTaskInfo != null);
        public void HighlightBadProcesses()
        {
            foreach (var taskInfo in TaskList)
            {
                if (taskInfo.TaskMemory > TaskService.RAMLimit(ComputerRAM, MainWindowViewModel.RAMLimitString))
                {
                    taskInfo.IsLimitExceeded = true;
                    SelectedTaskInfo = taskInfo;
                }
            }
        }
        public RelayCommand RefresfProcessesListCommand => new RelayCommand(obj => RefresfProcessesList());


        public void RefresfProcessesList()
        {
            var mainWindowViewModel = (MainWindowViewModel)App.Current.MainWindow.DataContext;
            mainWindowViewModel.SelectedViewModel = new ProcessesViewModel(ComputerIP, ComputerRAM);
        }
        public void ControlProcesses()
        {
            var taskResult = WebService.GetTasksAsync($"{PATH}/taskInfo");
            taskResult.Wait();
            var taskList = taskResult.Result;
            TaskList = new ObservableCollection<TaskInfo>(taskList);
        }   

        
        public ProcessesViewModel(string computerIP, float computerRam)
        {
            this.ComputerRAM = computerRam;
            this.ComputerIP = computerIP;
            PATH = $"http://{this.ComputerIP}:58415/api/tasks";
            ControlProcesses();
            HighlightBadProcesses();
        }
    }
}
