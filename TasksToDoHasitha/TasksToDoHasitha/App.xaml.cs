using FreshMvvm;
using System;
using System.IO;
using TasksToDoHasitha.PageModels;
using TasksToDoHasitha.Pages;
using TasksToDoHasitha.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TasksToDoHasitha
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            try
            {
                // Register the database service
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tasks.db3");
                var databaseService = new DatabaseService(dbPath);
                FreshIOC.Container.Register<IDatabaseService>(databaseService);


                // Register the initial page model
                var page = FreshPageModelResolver.ResolvePageModel<MainListViewPageModel>();
                var navigationPage = new FreshNavigationContainer(page);
                MainPage = navigationPage;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while initializing the app: {ex.Message}");
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
