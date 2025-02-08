using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TasksToDoHasitha.Models;

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
            throw new NotImplementedException();
        }

        public Task<List<TaskModel>> GetTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveTaskAsync(TaskModel task)
        {
            throw new NotImplementedException();
        }
    }
}
