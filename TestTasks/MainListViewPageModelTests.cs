using Moq;
using System.Collections.ObjectModel;
using TasksToDoHasitha.Models;
using TasksToDoHasitha.PageModels;
using TasksToDoHasitha.Services;

namespace TestTasks

{
    public class MainListViewPageModelTests
    {
        private readonly Mock<IDatabaseService> _mockDatabaseService;
        private readonly MainListViewPageModel _viewModel;

        public MainListViewPageModelTests()
        {
            _mockDatabaseService = new Mock<IDatabaseService>();
            _viewModel = new MainListViewPageModel(_mockDatabaseService.Object);
        }

        [Fact]
        public void InvalidSearchShouldReturnZero()
        {
            // Arrange
            var tasks = new List<TaskModel>
            {
                new TaskModel { Title = "Task 1", Description = "Description 1" },
                new TaskModel { Title = "Task 2", Description = "Description 2" }
            };
            _viewModel.Tasks = new ObservableCollection<TaskModel>(tasks);
            _viewModel.SearchQuery = "blah";

            // Act
            _viewModel.FilterTasks();

            // Assert
            Assert.Empty(_viewModel.FilteredTasks);
        }

        [Fact]
        public void ValidSearchResultsShouldReturnNonEmptyList()
        {
            // Arrange
            var tasks = new List<TaskModel>
            {
                new TaskModel { Title = "Task 1", Description = "Description 1" },
                new TaskModel { Title = "Task 2", Description = "Description 2" }
            };
            _viewModel.Tasks = new ObservableCollection<TaskModel>(tasks);
            _viewModel.SearchQuery = tasks[0].Title;

            // Act
            _viewModel.FilterTasks();

            // Assert
            Assert.NotEmpty(_viewModel.FilteredTasks);
        }


    }
}

