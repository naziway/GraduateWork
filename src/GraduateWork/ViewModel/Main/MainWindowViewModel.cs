using DatabaseService;
using PropertyChanged;
using Shared;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel.Find;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class MainWindowViewModel
    {
        public DataService DataService { get; set; }

        #region Action
        public Action<OpenWindow> OpenWindowAction { get; set; }
        public Action<OpenWindow, TransferData> OpenWindowWithEventResultAction { get; set; }

        public void SetAction(Action<OpenWindow, TransferData> action)
        {
            OpenWindowWithEventResultAction = action;
        }
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
        public ICommand AddNewSellingCommand => new CommandHandler(() =>
        {
            OpenWindowAction(OpenWindow.NewSelling);
        });
        public ICommand ClientListCommand => new CommandHandler(() =>
        {

        });

        public ICommand OrdersCommand => new CommandHandler(() =>
        {

            OpenWindowAction(OpenWindow.Repairs);
        });
        public ICommand OpenReviewListCommand => new CommandHandler(() =>
        {
            OpenWindowAction(OpenWindow.ListReview);
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