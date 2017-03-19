using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Shared;
using UserControls;
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
            MainWindowViewModel.SetAction(OpenResizeWindow);
            InvokeInMainThread(() =>
            {
                MainWindowView = new MainWindowView(MainWindowViewModel);
                MainWindowView.Show();
            });
        }
        private void OpenResizeWindow(OpenWindow windowType)
        {
            Window view;
            switch (windowType)
            {
                case Shared.OpenWindow.Orders:
                    view = new Base.ResizeBaseView(new OrdersUserControl(), "Список замовлень", 500, 500);
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
            InvokeInMainThread(LoginView.Close);
        }

        #endregion
    }
}