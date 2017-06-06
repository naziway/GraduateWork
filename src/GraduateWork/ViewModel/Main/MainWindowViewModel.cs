using DatabaseService;
using Model;
using PropertyChanged;
using Shared;
using Shared.Enum;
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
        public string ActiveUserFirstName => DataService.User.UserData.FirstName;
        public string ActiveUserLastName => DataService.User.UserData.LastName;

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
             new CustomMenuItem {Command =AddNewSellingCommand , Img = "Icons/add_icon.png" , ToolTip = "Додати Покупку"},
             new CustomMenuItem {Command =NewClientCommand ,     Img = "Icons/add_icon.png" , ToolTip = "Додати Нового Клієнта"},
             new CustomMenuItem {Command =AddNewDeviceCommand ,  Img = "Icons/icon_add.png" , ToolTip = "Додати Новий Пристрій"},
             new CustomMenuItem {Command =AddNewReviewCommand ,  Img = "Icons/icon_add.png" , ToolTip = "Додати Обстеження"},

             new CustomMenuItem {Command =ListReviewCommand ,    Img = "Icons/List.png" , ToolTip = "Список Обстежень"},
             new CustomMenuItem {Command =ListDevicesCommand ,    Img = "Icons/List.png" , ToolTip = "Список Пристроїв"},
             new CustomMenuItem {Command =OpenClientListCommand ,Img = "Icons/List.png" , ToolTip = "Список Клієнтів"},

             new CustomMenuItem {Command =OpenSalaryInfoCommand ,Img = "Icons/salary.png" , ToolTip = "Статистика заробітньої плати"},
             new CustomMenuItem {Command =IncomeCostCommand ,Img = "Icons/salary.png" , ToolTip = "Дохід/Витрати"},
             new CustomMenuItem {Command =LogOutCommand ,        Img = "Icons/LogOut.png",ToolTip = "Вихід"},
                 };


        }

        #region Command
        public ICommand ListDevicesCommand => new CommandHandler(() =>
        {
            OpenWindowAction(OpenWindow.ListDevices);
        });
        public ICommand OpenClientListCommand => new CommandHandler(() =>
        {
            OpenWindowAction(OpenWindow.ListClient);
        });
        public ICommand NewClientCommand => new CommandHandler(() =>
        {
            OpenWindowAction(OpenWindow.NewClient);
        });
        public ICommand OpenSalaryInfoCommand => new CommandHandler(() =>
        {
            OpenWindowAction(OpenWindow.SalaryInfo);
        });
        public ICommand IncomeCostCommand => new CommandHandler(() =>
        {
            OpenWindowAction(OpenWindow.SummaryІtatistics);
        });
        public ICommand AddNewDeviceCommand => new CommandHandler(() =>
        {
            OpenWindowAction(OpenWindow.NewDevice);
        });
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