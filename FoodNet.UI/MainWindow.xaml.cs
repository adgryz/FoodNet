using FoodNet.UI.ViewModel;
using System.Windows;
using System.Windows.Media;

namespace FoodNet.UI
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }
    }
}
