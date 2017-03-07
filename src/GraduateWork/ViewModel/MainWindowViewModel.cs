using PropertyChanged;
using Shared;
using System.Windows.Controls;
using System.Windows.Input;
using UserControls;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {

        }
        public UserControl CurrentUserControl { get; set; }

        public ICommand UsersCommand => new CommandHandler(() =>
        {
            CurrentUserControl = new UsersList();
        });
        public ICommand OrdersCommand => new CommandHandler(() =>
        {
            CurrentUserControl = new Orders();
        });


    }
}