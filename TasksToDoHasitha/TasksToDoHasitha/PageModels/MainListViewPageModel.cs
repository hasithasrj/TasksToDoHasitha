using FreshMvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using TasksToDoHasitha.Models;
using TasksToDoHasitha.Services;
using Xamarin.Forms;
using System.Linq;

namespace TasksToDoHasitha.PageModels
{
    public class MainListViewPageModel : FreshBasePageModel
    {
        private readonly IDatabaseService _databaseService;

        public ObservableCollection<TaskModel> Tasks { get; set; }

        public Command AddTaskCommand { get; }

        private TaskModel _selectedTask;
        public TaskModel SelectedTask
        {
            get => _selectedTask;
            set
            {
                if (_selectedTask != value)
                {
                    _selectedTask = value;
                    RaisePropertyChanged();
                }
            }
        }

        public MainListViewPageModel(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
            Tasks = new ObservableCollection<TaskModel>();
            
            AddTaskCommand = new Command(() => NavigateToAddTaskPage());

            LoadTasksAsync();
        }

        private void NavigateToAddTaskPage()
        {
            throw new NotImplementedException();
        }

        public void LoadTasksAsync()
        {
            Tasks = new ObservableCollection<TaskModel>
            {
                new TaskModel
                {
                    Id = 1,
                    Title = "Task 1",
                    Description = "Description for Task 1",
                    DueDate = DateTime.Now.AddDays(1),
                    IsCompleted = false
                },
                new TaskModel
                {
                    Id = 2,
                    Title = "Task 2",
                    Description = "Description for Task 2",
                    DueDate = DateTime.Now.AddDays(2),
                    IsCompleted = true
                },
                new TaskModel
                {
                    Id = 3,
                    Title = "Task 3",
                    Description = "Description for Task 3",
                    DueDate = DateTime.Now.AddDays(3),
                    IsCompleted = false
                }
            };
        }
    }
}
