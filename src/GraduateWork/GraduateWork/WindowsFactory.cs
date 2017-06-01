using DatabaseService;
using GraduateWork.Base;
using Model;
using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Windows;
using UserControls;
using UserControls.AddingControl;
using UserControls.ConvertControl;
using UserControls.InformationControl;
using UserControls.Lists;
using ViewModel;
using ViewModel.Lists;
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


        private void OpenResizeWindow(Shared.Enum.OpenWindow windowType)
        {
            Window view = new BaseView();
            switch (windowType)
            {
                case OpenWindow.NewSelling:
                    var sellingViewModel = new SellingCreatorViewModel(DataService);
                    sellingViewModel.SetAction(OpenWindowWithAction);
                    var sellingView = new SellingCreatorUserControl
                    {
                        DataContext = sellingViewModel,
                        RemoveBoxItemCommand = sellingViewModel.RemoveBoxItemCommand
                    };
                    view = new ResizeBaseView(sellingView, "Нова покупка", 600, 380);
                    break;
                case OpenWindow.ListClient:
                    var clientViewModel = new ClientListViewModel(DataService);
                    view = new ResizeBaseView(new ClientListUserControl { DataContext = clientViewModel }, "Список Клієнтів", 400, 500);
                    break;
                case OpenWindow.SalaryInfo:
                    var salaryViewModel = new SalaryViewModel(DataService);
                    view = new ResizeBaseView(new SalaryUserControl { DataContext = salaryViewModel }, "Статистика заробітньої плати", 500, 300);
                    break;
                case OpenWindow.ListReview:
                    var reviewsViewModel = new ReviewsViewModel(DataService);
                    reviewsViewModel.SetAction(OpenWindowWithData);
                    view = new ResizeBaseView(new ReviewListUserControl { DataContext = reviewsViewModel }, "Cписок Обстежень", 500, 500);
                    break;

                case OpenWindow.ListDevices:
                    var deviceListViewModel = new DeviceListViewModel(DataService);
                    view = new ResizeBaseView(new DeviceListUserControl { DataContext = deviceListViewModel }, "Cписок Пристроїв", 500, 500);
                    break;

                case OpenWindow.NewReview:
                    var viewModel = new NewReviewControl { DataContext = new NewReviewViewModel(DataService) };
                    view = new BaseView(viewModel, "Нове Обстеження", 300, 300);
                    break;
                case OpenWindow.NewClient:
                    var clientUserControl = new NewClientUserControl { DataContext = new NewClientViewModel(DataService) };
                    view = new BaseView(clientUserControl, "Новий Клієнт", 400, 300);
                    break;
                case OpenWindow.NewDevice:
                    var deviceUserControl = new NewDeviceUserControl { DataContext = new NewDeviceViewModel(DataService) };
                    view = new BaseView(deviceUserControl, "Новий Пристрій", 400, 300);
                    break;
                default: throw new InvalidOperationException();
            }
            OpenedWindows.Add(view);
            InvokeInMainThread(view.Show);
        }

        private void OpenWindowWithData(Shared.Enum.OpenWindow windowType, object data)
        {
            Window view;
            switch (windowType)
            {
                case OpenWindow.ReviewToOrder:
                    view = new ResizeBaseView(new ReviewToOrderView { DataContext = new ReviewToOrderViewModel(DataService, data as Review) }, "Обстеження", 500, 300);
                    break;
                case OpenWindow.RepairInfo:
                    view = new ResizeBaseView(new RepairUserControl { DataContext = new RepairViewModel(DataService, data as List<Repair>) }, "Ремонт", 500, 300);
                    break;
                default: throw new InvalidOperationException();
            }
            OpenedWindows.Add(view);
            InvokeInMainThread(view.Show);
        }
        private void OpenWindowWithAction(OpenWindow windowType, Action<object> action)
        {
            Window view;
            switch (windowType)
            {
                case OpenWindow.ListParts:

                    var listPartViewModel = new ListPartViewModel(DataService);
                    var listPart = new ListPart
                    {
                        DataContext = listPartViewModel,
                        Command = listPartViewModel.Command
                    };
                    listPartViewModel.SetAction(action);
                    view = new ResizeBaseView(listPart, "Cписок Запчастин", 500, 500);

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
            DataService.User = user;
            OpenMainWindow();

            InvokeInMainThread(LoginView.Close);
        }
        private void ViewModelOnFailedLogin(object sender, EventArgs e)
        {
            MessageBox.Show($"Failed Login Or Password");
        }
        #endregion
    }

}