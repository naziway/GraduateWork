using DatabaseService;
using GraduateWork.Base;
using Model;
using Shared;
using System;
using System.Collections.Generic;
using System.Windows;
using UserControls;
using UserControls.InformationControl;
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
            Window view = new BaseView();
            switch (windowType)
            {
                case Shared.OpenWindow.Repairs:
                    var orderViewModel = new RepairsViewModel(DataService);
                    orderViewModel.SetAction(OpenWindowWithData);
                    // view = new ResizeBaseView(new RepairsListWithFinding { DataContext = new RepairsWithFindViewModel(DataService, orderViewModel) }, "Список ремонтів", 500, 500);
                    break;
                case Shared.OpenWindow.NewReview:
                    var viewModel = new NewReviewControl { DataContext = new NewReviewViewModel(DataService) };
                    view = new BaseView(viewModel, "Нове Обстеження", 300, 300);
                    break;
                case Shared.OpenWindow.ListReview:
                    var reviewsViewModel = new ReviewsViewModel(DataService);
                    reviewsViewModel.SetAction(OpenWindowWithData);
                    view = new ResizeBaseView(new ReviewUserControl() { DataContext = reviewsViewModel }, "Cписок Обстежень", 500, 500);
                    break;
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
                case OpenWindow.ReviewToOrder:
                    view = new ResizeBaseView(new ReviewToOrderView { DataContext = new ReviewToOrderViewModel(DataService, data as Review) }, "Обстеження", 500, 300);
                    break;
                case OpenWindow.Repair:
                    view = new ResizeBaseView(new RepairUserControl{ DataContext = new RepairViewModel(DataService, data as List<Repair>) }, "Ремонт", 500, 300);
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

        #region Process Response Login

        private void ViewModelOnSuccessLogin(object sender, User user)
        {
            LoginViewModel.OnSuccessLogin -= ViewModelOnSuccessLogin;
            LoginViewModel.OnFailedLogin -= ViewModelOnFailedLogin;
            OpenMainWindow();
            DataService.User = user;
            InvokeInMainThread(LoginView.Close);
        }
        private void ViewModelOnFailedLogin(object sender, System.EventArgs e)
        {
            MessageBox.Show($"Failed Login Or Password");
        }
        #endregion
    }

}