using DatabaseService;
using PropertyChanged;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class ExaminateManager
    {
        public DataService Service { get; set; }

        public ExaminateManager(DataService service, ExaminateViewModel viewModel)
        {
            Service = service;
            ViewModel = viewModel;
        }

        public ExaminateViewModel ViewModel { get; set; }
    }
}