using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using ViewModel;

namespace GraduateWork
{
    public class WindowsFactory
    {
        private LoginViewModel LoginViewModel { get; set; } = new LoginViewModel();
        private LoginView LoginView { get; set; }
        private MainWindowViewModel MainWindowViewModel { get; set; } = new MainWindowViewModel();
        private MainWindowView MainWindowView { get; set; }

        private List<Window> OpenedWindows { get; } = new List<Window>();

        public WindowsFactory()
        {

        }
        public void OpenLoginWindow()
        {
            LoginViewModel.OnSuccessLogin += ViewModelOnSuccessLogin;
            LoginViewModel.OnFailedLogin += ViewModelOnFailedLogin;
            LoginView = new LoginView(LoginViewModel);
            InvokeInMainThread(LoginView.Show);
        }

        public void OpenMainWindow()
        {
            MainWindowViewModel.OnLogOut += MainWindowViewModelOnLogOut;
            MainWindowView = new MainWindowView(MainWindowViewModel);
            InvokeInMainThread(MainWindowView.Show);
        }


        //private IView OpenWindow(WindowType windowType)
        //{
        //    UserControl control;
        //    switch (windowType)
        //    {
        //        case WindowType.TimeAndSale:
        //            control = new TimeSellView(new TimeAndSaleViewModel(new TimeAndSaleService(stockManager)));
        //            break;
        //        case WindowType.TodayInfo:
        //            control = new TodayInfoView(new TodayInfoViewModel(new TodayInfoService(stockManager)));
        //            break;
        //        case WindowType.SendOrder:
        //            control = new SendOrderView(new SendOrderViewModel(executionService));
        //            break;
        //        case WindowType.AccountInfo:
        //            control = new AccountInfoView(new AccountInfoViewModel(accountService));
        //            break;
        //        case WindowType.Positions:
        //            control = new PositionsView(new PositionsViewModel(positionService));
        //            break;
        //        case WindowType.Pendings:
        //            control = new PendingView(new PendingViewModel(pendingService));
        //            break;
        //        case WindowType.Activity:
        //            control = new ActivityView(new ActivityViewModel(activityService));
        //            break;
        //        case WindowType.Level2:
        //            control = new Level2View(new Level2ViewModel(new Level2Service(stockManager)));
        //            break;
        //        case WindowType.RiskInfo:
        //            control = new RiskInfoView(new RiskInfoViewModel(accountService));
        //            break;
        //        case WindowType.Settings:
        //            control = new SettingsView(settingsViewModel);
        //            break;

        //        default: throw new InvalidOperationException();
        //    }
        //    IView view = new BaseWindow(control);
        //    view.OnViewClosed += WindowClosed;
        //    view.ChangeHeaderColor(settings.HeaderColor);
        //    OpenedWindows.Add(view);
        //    return view;
        //}

        private void InvokeInMainThread(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        private void MainWindowViewModelOnLogOut(object sender, EventArgs e)
        {
            OpenLoginWindow();
            InvokeInMainThread(MainWindowView.Close);
        }

        #region ProccessResponseLogin
        private void ViewModelOnFailedLogin(object sender, System.EventArgs e)
        {
            MessageBox.Show($"Failed Login Or Password");
        }
        private void ViewModelOnSuccessLogin(object sender, User user)
        {
            MessageBox.Show($"Success Log In {user.Login}");
            LoginViewModel.OnSuccessLogin -= ViewModelOnSuccessLogin;
            LoginViewModel.OnFailedLogin -= ViewModelOnFailedLogin;
            OpenMainWindow();
            InvokeInMainThread(LoginView.Close);
        }

        #endregion
    }
}