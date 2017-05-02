using DatabaseService;
using PropertyChanged;
using ViewModel.Base;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class OrderWithFindViewModel : FindViewModel
    {
        public DataService Service { get; set; }

        public OrderWithFindViewModel(DataService service, OrdersViewModel viewModel)
        {
            Service = service;
            ViewModel = viewModel;
        }

        public OrdersViewModel ViewModel { get; set; }
    }
}