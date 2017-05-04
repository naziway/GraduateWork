using DatabaseService;
using Model;
using PropertyChanged;
using Shared;
using Shared.Enum;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class ExaminateViewModel
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


        public ObservableCollection<OrderModel> Orders { get; set; }

        public ExaminateViewModel(DataService dataService)
        {
            DataService = dataService;
            var command = new CommandWithParameters(OpenOrderInfoWindow);
            var orders = DataService.GetAllExaminates();
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
            OpenWindowByDataAction(OpenWindow.ExaminateToOrder, order);
        }
    }
}