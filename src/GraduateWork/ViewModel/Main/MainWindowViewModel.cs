using DatabaseService;
using Model;
using PropertyChanged;
using Shared;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class MainWindowViewModel
    {
        public HistogramViewModel HistogramViewModel { get; set; }
        public List<CustomMenuItem> MenuItems { get; set; }
        public DataService DataService { get; set; }
        public string ActiveUser => DataService.User.Login;

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
            HistogramViewModel = new HistogramViewModel(dataService);
            MenuItems = new List<CustomMenuItem>
                {
             new CustomMenuItem {Command =AddNewSellingCommand , Img = "Icons/Plus.png" , ToolTip = "Додати Покупку"},
             new CustomMenuItem {Command =AddNewReviewCommand , Img = "Icons/Plus.png" , ToolTip = "Додати Обстеження"},
             new CustomMenuItem {Command =ListReviewCommand , Img = "Icons/List.png" , ToolTip = "Список Обстежень"},
             new CustomMenuItem {Command =LogOutCommand , Img = "Icons/LogOut.png" , ToolTip = "Вихід"},
                 };


        }

        #region Command
        public ICommand AddNewSellingCommand => new CommandHandler(() =>
        {
            OpenWindowAction(OpenWindow.NewSelling);
        });
        public ICommand AddNewReviewCommand => new CommandHandler(() =>
        {
            OpenWindowAction(OpenWindow.NewReview);
        });
        public ICommand ListReviewCommand => new CommandHandler(() =>
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