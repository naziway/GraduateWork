using DatabaseService;
using Model;
using PropertyChanged;
using Shared;
using Shared.Enum;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class SellingCreatorViewModel
    {
        public DataService DatabaseService { get; set; }

        public ObservableCollection<PartModel> ChooseParts { get; set; } = new ObservableCollection<PartModel>();

        public Action<OpenWindow, Action<object>> OpenWindowWithAction { get; set; }
        public ICommand RemoveBoxItemCommand => new CommandWithParameters(part =>
        {
            var item = part as PartModel;
            if (item != null)
                ChooseParts.Add(item);
            foreach (var choosePart in ChooseParts)
            {
                SellingSumma += choosePart.Price * choosePart.ChooseCount;
            }
        });
        public ICommand OpenPartViewCommand => new CommandHandler(() =>
        {
            OpenWindowWithAction.Invoke(OpenWindow.ListParts, NewPart);
        });
        public double SellingSumma { get; set; }
        public SellingCreatorViewModel(DataService databaseService)
        {
            DatabaseService = databaseService;

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

        public void SetAction(Action<OpenWindow, Action<object>> openWindowWithAction)
        {
            OpenWindowWithAction = openWindowWithAction;
        }

        private void NewPart(object obj)
        {
            var part = obj as PartModel;
            if (part == null) return;
            ChooseParts.Add(new PartModel
            {
                Id = part.Id,
                Price = part.Price,
                Marka = part.Marka,
                AvailableCount = part.AvailableCount,
                ChooseCount = part.ChooseCount,
                Model = part.Model,
                Title = part.Title
            });
            SellingSumma = 0;
            foreach (var choosePart in ChooseParts)
            {
                SellingSumma += choosePart.Price * choosePart.ChooseCount;
            }
        }
    }
}