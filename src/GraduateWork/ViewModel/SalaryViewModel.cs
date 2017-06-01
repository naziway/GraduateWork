using DatabaseService;
using Model;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class SalaryViewModel
    {
        public DataService Service { get; set; }

        public SalaryViewModel(DatabaseService.DataService service)
        {
            Service = service;
            var data = new HistogramLogic(service).GetSalaryStatisticByUser(service.User);
            Items = new ObservableCollection<SalaryHistogramModel>(data);
        }
        public ObservableCollection<SalaryHistogramModel> Items { get; set; }

    }
}