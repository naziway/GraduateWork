using DatabaseService;
using PropertyChanged;
using ViewModel.Base;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class OrderWithFindViewModel : FindViewModel
    {
        public DataService Service { get; set; }

        public OrderWithFindViewModel(DataService service, RepairsViewModel viewModel)
        {
            Service = service;
            ViewModel = viewModel;
        }

        public RepairsViewModel ViewModel { get; set; }
    }
}