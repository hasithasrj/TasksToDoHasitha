using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TasksToDoHasitha.Models;
using Xamarin.Essentials;

namespace TasksToDoHasitha.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly SQLiteAsyncConnection _database;
        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TaskModel>().Wait();
        }

        public Task<int> DeleteTaskAsync(TaskModel task)
        {
            return _database.DeleteAsync(task);
        }

        public Task<List<TaskModel>> GetTasksAsync()
        {
            return _database.Table<TaskModel>().ToListAsync();
        }

        public Task<int> SaveTaskAsync(TaskModel task)
        {
            if (task.Id != 0)
            {
                return _database.UpdateAsync(task);
            }
            else
            {
                return _database.InsertAsync(task);
            }
        }
    }
}
