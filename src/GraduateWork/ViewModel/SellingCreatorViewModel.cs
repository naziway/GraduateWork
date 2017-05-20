using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DatabaseService;
using Model;
using PropertyChanged;
using Shared;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class SellingCreatorViewModel
    {
        public DataService DatabaseService { get; set; }
        public ObservableCollection<PartModel> Parts { get; set; }
        public ObservableCollection<PartModel> ChooseParts { get; set; } = new ObservableCollection<PartModel>();

        public ICommand AddPart => new CommandWithParameters(part =>
        {
            var item = part as PartModel;
            if (item != null)
                ChooseParts.Add(item);
            foreach (var choosePart in ChooseParts)
            {
                SellingSumma += choosePart.Price * choosePart.ChooseCount;
            }
        });
        public double SellingSumma { get; set; }
        public SellingCreatorViewModel(DataService databaseService)
        {
            DatabaseService = databaseService;
            Task.Factory.StartNew(() => { Parts = new ObservableCollection<PartModel>(DatabaseService.GetParts().Select(Convert)); });
        }

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