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
                    findOrders = orders.Where(order => order.Client.Name.Contains(FindText) ||
                    order.Client.Name.Contains(FindText) ||
                    order.Client.Surname.Contains(FindText) ||
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
            Orders = new ObservableCollection<OrderModel>(DataService.GetAllOrders());
        }
    }
}