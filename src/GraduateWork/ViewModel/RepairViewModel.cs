using System.Collections.Generic;
using System.Collections.ObjectModel;
using DatabaseService;
using Model;
using PropertyChanged;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class RepairViewModel
    {
        public DataService DataService { get; set; }
        public ObservableCollection<Repair> Repairs { get; set; }

        public RepairViewModel(DataService dataService, List<Repair> repair)
        {
            DataService = dataService;
            Repairs = new ObservableCollection<Repair>(repair);
        }
    }
}