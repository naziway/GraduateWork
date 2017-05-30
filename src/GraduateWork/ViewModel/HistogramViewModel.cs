using DatabaseService;
using Model;
using System.Collections.Generic;

namespace ViewModel
{
    public class HistogramViewModel
    {
        public DataService Service { get; set; }


        public List<HistogramModel> Items { get; set; }
        public HistogramViewModel(DatabaseService.DataService service)
        {
            Service = service;
            HistogramLogic = new HistogramLogic(service);

            Items = HistogramLogic.GetStatisticList();
        }

        public HistogramLogic HistogramLogic { get; set; }
    }
}