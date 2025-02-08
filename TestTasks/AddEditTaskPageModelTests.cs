using Xunit;
using Moq;
using System.Threading.Tasks;
using TasksToDoHasitha.PageModels;
using TasksToDoHasitha.Services;
using TasksToDoHasitha.Models;
using FreshMvvm;

namespace TasksToDoHasitha.Tests.PageModels
{
    public class AddEditTaskPageModelTests
    {
        private Mock<IDatabaseService> _mockDatabaseService;
        private Mock<IPageModelCoreMethods> _mockCoreMethods;
        private AddEditTaskPageModel _pageModel;

        public AddEditTaskPageModelTests()
        {
            _mockDatabaseService = new Mock<IDatabaseService>();
            _mockCoreMethods = new Mock<IPageModelCoreMethods>();
            _pageModel = new AddEditTaskPageModel(_mockDatabaseService.Object)
            {
                CoreMethods = _mockCoreMethods.Object
            };
        }

        [Fact]
        public async Task SaveTaskCommand_ShouldDisplayError_WhenTitleIsEmpty()
        {
            // Arrange
            _pageModel.Task = new TaskModel { Title = "" };

            // Act
            await _pageModel.SaveTask();

            // Assert
            _mockCoreMethods.Verify(m => m.DisplayAlert("Error", "Title is required.", "OK"), Times.Once);
            _mockDatabaseService.Verify(m => m.SaveTaskAsync(It.IsAny<TaskModel>()), Times.Never);
        }

        [Fact]
        public async Task SaveTaskCommand_ShouldSaveTask_WhenTitleIsNotEmpty()
        {
            // Arrange
            _pageModel.Task = new TaskModel { Title = "Test Task" };

            // Act
            await _pageModel.SaveTask();

            // Assert
            _mockDatabaseService.Verify(m => m.SaveTaskAsync(_pageModel.Task), Times.Once);
            _mockCoreMethods.Verify(m => m.PopPageModel(_pageModel.Task, false, true), Times.Once);
        }
    }
}
