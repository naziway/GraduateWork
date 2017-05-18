using DatabaseService;
using PropertyChanged;
using ViewModel.Base;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class RepairsWithFindViewModel : FindViewModel
    {
        public DataService Service { get; set; }

        public RepairsWithFindViewModel(DataService service, RepairsViewModel viewModel)
        {
            Service = service;
            ViewModel = viewModel;
        }

        public RepairsViewModel ViewModel { get; set; }
    }
}