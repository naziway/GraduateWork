using DatabaseService;
using PropertyChanged;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class ExaminateManager
    {
        public DataService Service { get; set; }

        public ExaminateManager(DataService service, ReviewsViewModel viewModel)
        {
            Service = service;
            ViewModel = viewModel;
        }

        public ReviewsViewModel ViewModel { get; set; }
    }
}