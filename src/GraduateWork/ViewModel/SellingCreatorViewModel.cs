using DatabaseService;
using Model;
using PropertyChanged;
using Shared;
using Shared.Enum;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class SellingCreatorViewModel
    {
        public DataService DatabaseService { get; set; }
        public ObservableCollection<PartModel> ChooseParts { get; set; } = new ObservableCollection<PartModel>();
        public ObservableCollection<Client> Clients { get; set; }
        public Client SelectedClient { get; set; }
        public double SellingSumma { get; set; }
        public Action<OpenWindow, Action<object>> OpenWindowWithAction { get; set; }

        public ICommand ClearChoisePartListCommand => new CommandHandler(() => ChooseParts.Clear());
        public ICommand ApplayCommand => new CommandHandler(async () =>
        {
            if (ChooseParts.Count < 1)
                return;

            var sellings = ChooseParts.Select(Convert).Select(choosePart => new Selling
            {
                Part = choosePart,
                Client = SelectedClient,
                OrderDate = DateTime.Now,
                Status = SellingStatus.New
            }).ToList();

            await DatabaseService.AddSellings(sellings);

        });
        public ICommand RemoveBoxItemCommand => new CommandWithParameters(part =>
        {
            var item = part as PartModel;
            if (item == null)
                return;
            ChooseParts.Remove(item);
            foreach (var choosePart in ChooseParts)
            {
                SellingSumma += choosePart.Price * choosePart.ChooseCount;
            }
        });
        public ICommand OpenPartViewCommand => new CommandHandler(() =>
        {
            OpenWindowWithAction.Invoke(OpenWindow.ListParts, NewPart);
        });

        public SellingCreatorViewModel(DataService databaseService)
        {
            DatabaseService = databaseService;
            Clients = new ObservableCollection<Client>(DatabaseService.GetClients());
            if (Clients.Any())
                SelectedClient = Clients.First();
        }
        public void SetAction(Action<OpenWindow, Action<object>> openWindowWithAction)
        {
            OpenWindowWithAction = openWindowWithAction;
        }
        private void NewPart(object obj)
        {
            var newPart = obj as PartModel;
            if (newPart == null)
                return;
            var currentPart = ChooseParts.FirstOrDefault(model => model.Id == newPart.Id);
            if (currentPart == null)
            {
                if (newPart.ChooseCount <= newPart.AvailableCount)
                    ChooseParts.Add(new PartModel
                    {
                        Id = newPart.Id,
                        Price = newPart.Price,
                        Marka = newPart.Marka,
                        AvailableCount = newPart.AvailableCount,
                        ChooseCount = newPart.ChooseCount,
                        Model = newPart.Model,
                        Title = newPart.Title
                    });
            }
            else if (currentPart.ChooseCount + newPart.ChooseCount <= currentPart.AvailableCount)
                currentPart.ChooseCount += newPart.ChooseCount;

            SellingSumma = 0;
            foreach (var choosePart in ChooseParts)
            {
                SellingSumma += choosePart.Price * choosePart.ChooseCount;
            }
        }

        private PartModel Convert(Part part) => new PartModel
        {
            Id = part.Id,
            Model = part.Model,
            Title = part.Title,
            Price = part.Price,
            Marka = part.Marka,
            AvailableCount = part.Count
        };
        private Part Convert(PartModel part) => new Part
        {
            Id = part.Id,
            Model = part.Model,
            Title = part.Title,
            Price = part.Price,
            Marka = part.Marka,
            Count = part.ChooseCount
        };

    }
}