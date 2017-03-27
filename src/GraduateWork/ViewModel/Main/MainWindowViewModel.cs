using DatabaseService;
using PropertyChanged;
using Shared;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using UserControls;
using ViewModel.Find;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class MainWindowViewModel
    {
        public DataService DataService { get; set; }

        #region Action
        public Action<OpenWindow> OpenWindowAction { get; set; }

        public void SetAction(Action<OpenWindow> action)
        {
            OpenWindowAction = action;
        }
        #endregion

        public UserControl CurrentUserControl { get; set; }

        public MainWindowViewModel(DataService dataService)
        {
            DataService = dataService;
        }

        #region Command
        public ICommand AddNewExaminationCommand => new CommandHandler(() =>
        {
            //CurrentUserControl = new AddNewExaminateControl();
            OpenWindowAction(OpenWindow.NewExaminate);
        });
        public ICommand ClientListCommand => new CommandHandler(() =>
        {
            var userControl = new ClientListWithFinding { DataContext = new ClientFindingViewModel(DataService) };
            CurrentUserControl = userControl;
        });

        public ICommand OrdersCommand => new CommandHandler(() =>
        {
            CurrentUserControl = new OrdersUserControl();
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