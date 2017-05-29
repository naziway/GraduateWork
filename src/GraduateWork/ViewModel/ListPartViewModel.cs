using DatabaseService;
using Model;
using Shared;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class ListPartViewModel
    {
        public DataService Service { get; set; }

        public ObservableCollection<PartModel> Parts { get; set; }

        public ListPartViewModel(DataService service)
        {
            Service = service;
            Task.Factory.StartNew(() => { Parts = new ObservableCollection<PartModel>(Service.GetParts().Select(Convert)); });
        }

        public void SetAction(Action<object> action)
        {
            AddAction = action;
        }

        public Action<object> AddAction { get; set; }
        public ICommand Command => new CommandWithParameters(o =>
        {
            var part = o as PartModel;
            if (part != null)
                AddAction.Invoke(part);
        });
        private static PartModel Convert(Part part) => new PartModel
        {
            Id = part.Id,
            Model = part.Model,
            Title = part.Title,
            Price = part.Price,
            Marka = part.Marka,
            AvailableCount = part.Count
        };
    }
}