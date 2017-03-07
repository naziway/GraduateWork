using System.Windows;
using ViewModel;

namespace GraduateWork
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        private readonly MainWindowViewModel ViewModel;

        public MainWindowView(MainWindowViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
