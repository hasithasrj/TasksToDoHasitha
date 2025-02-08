using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TasksToDoHasitha.Models;
using TasksToDoHasitha.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TasksToDoHasitha.PageModels
{
    public class AddEditTaskPageModel : FreshBasePageModel
    {
        private readonly IDatabaseService _databaseService;

        public TaskModel Task { get; set; }

        public Command SaveTaskCommand { get; }
        public Command DeleteTaskCommand { get; }

        public AddEditTaskPageModel(IDatabaseService databaseService)
        {
            _databaseService = databaseService;

            SaveTaskCommand = new Command(async () => await SaveTask());
            DeleteTaskCommand = new Command(async () => await DeleteTask());
        }

        public override void Init(object initData)
        {
            if (initData is TaskModel task)
            {
                Task = task;
            }
            else
            {
                Task = new TaskModel { DueDate = DateTime.Now };
            }
        }

        private async Task SaveTask()
        {
            if (string.IsNullOrWhiteSpace(Task.Title))
            {
                await CoreMethods.DisplayAlert("Error", "Title is required.", "OK");
                return;
            }

            await _databaseService.SaveTaskAsync(Task);
            await CoreMethods.PopPageModel(Task);
        }

        private async Task DeleteTask()
        {
            throw new NotImplementedException();
        }
    }
}
