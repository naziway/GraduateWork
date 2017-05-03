using System;
using DatabaseService;
using Model;
using Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ViewModel
{
    public class OrdersViewModel
    {
        #region Action
        public Action<OpenWindow> OpenWindowAction { get; set; }
        public Action<OpenWindow, object> OpenWindowByDataAction { get; set; }
        public Action<OpenWindow, TransferData> OpenWindowWithEventResultAction { get; set; }

        public void SetAction(Action<OpenWindow, TransferData> action)
        {
            OpenWindowWithEventResultAction = action;
        }
        public void SetAction(Action<OpenWindow> action)
        {
            OpenWindowAction = action;
        }
        public void SetAction(Action<OpenWindow, object> action)
        {
            OpenWindowByDataAction = action;
        }
        #endregion
        public DataService DataService { get; set; }

        public string FindText { get; set; }
        public string SelectedParam { get; set; }


        public ICommand FindCommand => new CommandHandler(Finding);

        private void Finding()
        {
            var orders = DataService.GetAllOrders();
            List<OrderModel> findOrders = new List<OrderModel>();
            switch (SelectedParam)
            {
                case "Все":
                    findOrders = orders.Where(order =>
                    order.Device.PhoneModel.Contains(FindText) ||
                    order.OrderKod.ToString().Contains(FindText)).ToList();
                    break;
                case "Імя":
                    break;
                case "Прізвище":
                    break;
                case "Ціна":
                    break;
            }

            Orders = new ObservableCollection<OrderModel>(findOrders);
        }

        public ObservableCollection<OrderModel> Orders { get; set; }

        public ObservableCollection<string> FindingParametersList
            => new ObservableCollection<string>()
            {
                "Імя",
                "Прізвище",
                "Ціна",
                "Все"
            };
        public OrdersViewModel(DataService dataService)
        {
            DataService = dataService;
            var command = new CommandWithParameters(OpenOrderInfoWindow);
            var orders = DataService.GetAllOrders();
            Command = command;
            orders.ForEach((order) => { order.Command = command; });
            Orders = new ObservableCollection<OrderModel>(orders);
        }
        public ICommand Command { get; set; }
        private void OpenOrderInfoWindow(object obj)
        {
            var order = obj as OrderModel;
            if (order == null)
                return;
            OpenWindowByDataAction(OpenWindow.OrderInfo, order);
        }
    }
}