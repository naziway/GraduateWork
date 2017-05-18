using DatabaseService;
using Model;

namespace ViewModel
{
    public class OrderInfoViewModel
    {
        public DataService Service { get; set; }
        public OrderModel Model { get; set; }

        public OrderInfoViewModel(DataService service, OrderModel model)
        {
            Service = service;
            Model = model;
        }
    }
}