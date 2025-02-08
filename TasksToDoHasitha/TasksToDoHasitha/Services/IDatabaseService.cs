using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using TasksToDoHasitha.Models;
using Xamarin.Essentials;

namespace TasksToDoHasitha.Services
{
    public interface IDatabaseService
    {
        Task<List<TaskModel>> GetTasksAsync();
        Task<int> SaveTaskAsync(TaskModel task);
        Task<int> DeleteTaskAsync(TaskModel task);
    }
}
