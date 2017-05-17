using DatabaseService;
using Model;
using PropertyChanged;
using Shared;
using System;
using System.Collections.ObjectModel;
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


        public ObservableCollection<Review> Reviews { get; set; }

        public ExaminateViewModel(DataService dataService)
        {
            DataService = dataService;
            var command = new CommandWithParameters(OpenOrderInfoWindow);
            var list = DataService.GetReviews();
            Command = command;
            list.ForEach((order) => { order.Command = command; });
            Reviews = new ObservableCollection<Review>(list);
        }

        public ICommand Command { get; set; }
        private void OpenOrderInfoWindow(object obj)
        {
            //var order = obj as OrderModel;
            //if (order == null)
            //    return;
            //OpenWindowByDataAction(OpenWindow.ReviewToOrder, order);
        }
    }
}