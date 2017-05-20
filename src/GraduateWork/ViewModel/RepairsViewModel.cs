using DatabaseService;
using Model;
using Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ViewModel
{
    public class RepairsViewModel
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
            var repairs = DataService.GetRepairs();
            List<Repair> list = new List<Repair>();
            switch (SelectedParam)
            {
                case "Все":
                    list = repairs.Where(order =>
                    order.Device.Marka.Contains(FindText) ||
                    order.Kod.ToString().Contains(FindText)).ToList();
                    break;
                case "Імя":
                    break;
                case "Прізвище":
                    break;
                case "Ціна":
                    break;
            }

            Repairs = new ObservableCollection<Repair>(list);
        }

        public ObservableCollection<Repair> Repairs { get; set; }

        public ObservableCollection<string> FindingParametersList
            => new ObservableCollection<string>()
            {
                "Імя",
                "Прізвище",
                "Ціна",
                "Все"
            };
        public RepairsViewModel(DataService dataService)
        {
            DataService = dataService;
            var command = new CommandWithParameters(OpenOrderInfoWindow);
            var orders = DataService.GetRepairs();
            Command = command;
            //orders.ForEach((order) => { order.Command = command; });
            Repairs = new ObservableCollection<Repair>(orders);
        }

        public ICommand Command { get; set; }
        private void OpenOrderInfoWindow(object obj)
        {
            //   var order = obj as OrderModel;
            //   if (order == null)
            //       return;
            //   OpenWindowByDataAction(OpenWindow.OrderInfo, order);
        }
    }
}