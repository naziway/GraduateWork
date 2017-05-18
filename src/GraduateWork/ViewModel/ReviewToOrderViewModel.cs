using DatabaseService;
using Model;
using PropertyChanged;
using System.Collections.ObjectModel;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class ReviewToOrderViewModel
    {
        public DataService DataService { get; set; }
        public Review Review { get; set; }
        public Device Device { get; set; }
        public User Worker { get; set; }

        ObservableCollection<Repair> Repairs { get; set; } = new ObservableCollection<Repair>();
        ObservableCollection<Part> Parts { get; set; }
        ObservableCollection<Works> Works { get; set; }

        public ReviewToOrderViewModel(DataService dataService, Review review)
        {
            DataService = dataService;
            Review = review;
        }





    }
}