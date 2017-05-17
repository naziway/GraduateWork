using DatabaseService;
using GraduateWork.Base;
using Model;
using Shared;
using System;
using System.Collections.Generic;
using System.Windows;
using UserControls;
using ViewModel;
using ViewModel.ResourseAdd;

namespace GraduateWork
{
    public class WindowsFactory
    {
        private LoginViewModel LoginViewModel { get; set; } = new LoginViewModel();
        private LoginView LoginView { get; set; }
        private MainWindowViewModel MainWindowViewModel { get; set; }
        private ResizeBaseView MainWindowView { get; set; }
        private DataService DataService { get; set; }

        private List<Window> OpenedWindows { get; } = new List<Window>();

        public WindowsFactory()
        {
            DataService = new DataService();
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
            MainWindowViewModel = new MainWindowViewModel(DataService);
            MainWindowViewModel.OnLogOut += MainWindowViewModelOnLogOut;
            MainWindowViewModel.SetAction(OpenResizeWindow);
            InvokeInMainThread(() =>
            {
                MainWindowView = new ResizeBaseView(new MainWindowView(MainWindowViewModel), "Головне Вікно", 600, 900);
                MainWindowView.Show();
            });
        }
        private void OpenResizeWindow(OpenWindow windowType)
        {
            Window view;
            switch (windowType)
            {
                case Shared.OpenWindow.Orders:
                    var orderViewModel = new RepairsViewModel(DataService);
                    orderViewModel.SetAction(OpenWindowWithData);
                    view = new ResizeBaseView(new OrdersListWithFinding { DataContext = new OrderWithFindViewModel(DataService, orderViewModel) }, "Список замовлень", 500, 500);
                    break;
                case Shared.OpenWindow.NewExaminate:
                    var viewModel = new AddNewExaminateControl { DataContext = new NewReviewViewModel(DataService) };
                    view = new BaseView(viewModel, "Нова Діагностика", 300, 300);
                    break;
                case Shared.OpenWindow.ListExaminate:
                    var examToOrderViewModel = new ExaminateViewModel(DataService);// new ExaminateManager(DataService, new ExaminateViewModel(DataService));
                    examToOrderViewModel.SetAction(OpenWindowWithData);
                    view = new ResizeBaseView(new OrdersUserControl() { DataContext = examToOrderViewModel }, "список обстежень", 500, 500);
                    break;
                default: throw new InvalidOperationException();
            }
            OpenedWindows.Add(view);
            InvokeInMainThread(view.Show);
        }
        private void OpenRezultResizeWindow(OpenWindow windowType, TransferData transferData)
        {
            Window view;
            switch (windowType)
            {
                default: throw new InvalidOperationException();
            }
            OpenedWindows.Add(view);
            InvokeInMainThread(view.Show);
        }
        private void OpenWindowWithData(OpenWindow windowType, object data)
        {
            Window view;
            switch (windowType)
            {
                case OpenWindow.OrderInfo:
                    var orderInfoViewModel = new OrderInfoViewModel(DataService, data as OrderModel);
                    view = new ResizeBaseView(new OrderInfo() { DataContext = orderInfoViewModel }, "Опис Замовлення", 500, 500);
                    break;
                case OpenWindow.ExaminateToOrder:
                    var examToOrderViewModel = new ExaminateManager(DataService, new ExaminateViewModel(DataService));
                    view = new ResizeBaseView(new ExaminateToOrder { DataContext = examToOrderViewModel }, "до ордеру", 500, 500);
                    break;
                default: throw new InvalidOperationException();
            }
            OpenedWindows.Add(view);
            InvokeInMainThread(view.Show);
        }

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
            DataService.User = user;
            InvokeInMainThread(LoginView.Close);
        }

        #endregion
    }
}