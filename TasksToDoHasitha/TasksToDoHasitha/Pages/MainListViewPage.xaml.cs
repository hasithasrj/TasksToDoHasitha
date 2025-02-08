using FreshMvvm;
using System.Threading.Tasks;
using TasksToDoHasitha.PageModels;
using Xamarin.Forms.Xaml;

namespace TasksToDoHasitha.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainListViewPage : FreshBaseContentPage
    {
        public MainListViewPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is MainListViewPageModel viewModel)
            {
                // Refresh the task list whenever the page appears
                Task.Run(async () => await viewModel.LoadTasksAsync());
            }
        }
    }
}