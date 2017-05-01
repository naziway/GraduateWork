using DatabaseService;
using PropertyChanged;
using ViewModel.Base;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class OrderWithFindViewModel : FindViewModel
    {
        public DataService Service { get; set; }

        public OrderWithFindViewModel(DataService service)
        {
            Service = service;
            ViewModel = new OrdersViewModel(Service);
        }

        public OrdersViewModel ViewModel { get; set; }
    }
}