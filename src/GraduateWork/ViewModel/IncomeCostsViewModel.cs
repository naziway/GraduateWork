using DatabaseService;
using Model;
using PropertyChanged;
using System.Collections.ObjectModel;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class IncomeCostsViewModel
    {
        public DataService Service { get; set; }

        public IncomeCostsViewModel(DatabaseService.DataService service)
        {
            Service = service;
            var data = new HistogramLogic(Service).GetIncomeCosts();
            Items = new ObservableCollection<CircleDiagramItem>(data);
        }

        public ObservableCollection<CircleDiagramItem> Items { get; set; }
    }
}