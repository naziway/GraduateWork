using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Model;
using ViewModel;

namespace GraduateWork
{
    public class WindowsFactory
    {
        private LoginView LoginView { get; set; }
        private List<Window> OpenedWindows { get; } = new List<Window>();

        public WindowsFactory()
        {
            InitializeServices();
        }
        public void OpenLoginWindow()
        {
            var viewModel = new LoginViewModel();
            viewModel.OnSuccessLogin += ViewModelOnSuccessLogin;
            viewModel.OnFailedLogin += ViewModelOnFailedLogin;
            LoginView = new LoginView(viewModel);
            LoginView.Show();
        }

        private void ViewModelOnFailedLogin(object sender, System.EventArgs e)
        {
            MessageBox.Show($"Failed Login Or Password");
        }

        private void ViewModelOnSuccessLogin(object sender, User user)
        {
            MessageBox.Show($"Success Log In {user.Login}");
            LoginView.Close();
        }

        private void ChangeHeaderColor(object sender, Color color)
        {

        }
        private void InitializeServices()
        {

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
        //private void WindowClosed(object sender, EventArgs e)
        //{
        //    var view = sender as IView;
        //    if (view == null)
        //        return;
        //    view.OnViewClosed -= WindowClosed;
        //    OpenedWindows.Remove(view);
        //}
        //private void CloseAllWindows(object sender, EventArgs e)
        //{
        //    foreach (var view in OpenedWindows)
        //    {
        //        view.OnViewClosed -= WindowClosed;
        //        view.CloseView();
        //    }
        //}
    }
}