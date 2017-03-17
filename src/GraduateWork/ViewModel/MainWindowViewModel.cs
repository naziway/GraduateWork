using PropertyChanged;
using Shared;
using System;
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
        public ICommand LogOutCommand => new CommandHandler(DoOnLogOut);



        public event EventHandler OnLogOut;

        protected virtual void DoOnLogOut()
        {
            OnLogOut?.Invoke(this, EventArgs.Empty);
        }
    }
}