using System.Windows;
using System.Windows.Input;
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

        private void UIElement_OnMouseLeftButtonDownuseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
