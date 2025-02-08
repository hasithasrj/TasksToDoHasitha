using FreshMvvm;
using System.Collections.ObjectModel;
using System;
using TasksToDoHasitha.Models;
using TasksToDoHasitha.Services;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace TasksToDoHasitha.PageModels
{
    public class MainListViewPageModel : FreshBasePageModel
    {
        private readonly IDatabaseService _databaseService;

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    RaisePropertyChanged();
                    FilterTasks();
                }
            }
        }

        public ObservableCollection<TaskModel> Tasks { get; set; }

        public ObservableCollection<TaskModel> FilteredTasks { get; set; }

        public Command AddTaskCommand { get; }

        public Command<TaskModel> SelectTaskCommand { get; }

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

                    if (_selectedTask != null)
                    {
                        // Handle the selected task
                        _ = NavigateToEditTaskPage(_selectedTask);

                        // Clear the selection
                        _selectedTask = null;
                        RaisePropertyChanged(nameof(SelectedTask));
                    }
                }
            }
        }

        public MainListViewPageModel(IDatabaseService databaseService)
        {
            try
            {
                _databaseService = databaseService;
                Tasks = new ObservableCollection<TaskModel>();
                FilteredTasks = new ObservableCollection<TaskModel>();

                AddTaskCommand = new Command(async () => await NavigateToAddTaskPage());
                SelectTaskCommand = new Command<TaskModel>(async (task) => await NavigateToEditTaskPage(task));

                _ = LoadTasksAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void FilterTasks()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                FilteredTasks = new ObservableCollection<TaskModel>(Tasks);
            }
            else
            {
                var filtered = Tasks.Where(t =>
                    (!string.IsNullOrWhiteSpace(t.Title) && t.Title.ToLowerInvariant().Contains(SearchQuery.ToLowerInvariant())) ||
                    (!string.IsNullOrWhiteSpace(t.Description) && t.Description.ToLowerInvariant().Contains(SearchQuery.ToLowerInvariant()))
                );

                FilteredTasks = new ObservableCollection<TaskModel>(filtered);
            }
            RaisePropertyChanged(nameof(FilteredTasks));
        }

        private async Task NavigateToAddTaskPage()
        {
            await CoreMethods.PushPageModel<AddEditTaskPageModel>();
        }

        private async Task NavigateToEditTaskPage(TaskModel task)
        {
            try
            {
                if (task == null)
                    return;

                await CoreMethods.PushPageModel<AddEditTaskPageModel>(task);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task LoadTasksAsync()
        {
            var tasks = await _databaseService.GetTasksAsync();
            Tasks = new ObservableCollection<TaskModel>(tasks);

            FilterTasks();
        }
    }
}
