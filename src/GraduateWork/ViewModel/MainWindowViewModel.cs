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
        #region Action
        public Action<OpenWindow> OpenWindowAction { get; set; }

        public void SetAction(Action<OpenWindow> action)
        {
            OpenWindowAction = action;
        }
        #endregion
        public UserControl CurrentUserControl { get; set; }

        public MainWindowViewModel()
        {

        }


        #region Command
        public ICommand UsersCommand => new CommandHandler(() =>
        {
            CurrentUserControl = new UsersList();
        });
        public ICommand OrdersCommand => new CommandHandler(() =>
        {
            CurrentUserControl = new Orders();
            OpenWindowAction(OpenWindow.Orders);
        });
        public ICommand LogOutCommand => new CommandHandler(DoOnLogOut);
        #endregion


        public event EventHandler OnLogOut;

        protected virtual void DoOnLogOut()
        {
            OnLogOut?.Invoke(this, EventArgs.Empty);
        }
    }
}