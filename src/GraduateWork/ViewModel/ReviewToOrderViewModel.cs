using DatabaseService;
using Model;
using PropertyChanged;
using Shared;
using Shared.Enum;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class ReviewToOrderViewModel
    {
        public DataService DataService { get; set; }
        public Review Review { get; set; }
        public CheckManager CheckManager { get; set; }
        public Part SelectedPart { get; set; }
        public Work SelectedWork { get; set; }
        public double Symma { get; set; }

        public ObservableCollection<Repair> Repairs { get; set; } = new ObservableCollection<Repair>();
        public ObservableCollection<Part> Parts { get; set; }
        public ObservableCollection<Work> Works { get; set; }

        public ReviewToOrderViewModel(DataService dataService, Review review)
        {
            DataService = dataService;
            Review = review;
            CheckManager = new CheckManager(dataService);
            Parts = new ObservableCollection<Part>(DataService.GetParts());
            Works = new ObservableCollection<Work>(DataService.GetWorks());
        }
        public ICommand AddRepair => new CommandHandler(() =>
        {
            Repairs.Add(new Repair
            {
                Work = SelectedWork,
                Part = SelectedPart,
                Device = Review.Device,
                OrderDate = DateTime.Now,
                Status = RepairStatus.Сформований,
                Worker = Review.Worker
            });
            Symma += SelectedPart.Price + SelectedWork.Price;// частина може бути null
        });
        public ICommand SaveReviewInRepair => new CommandHandler(async () =>
        {
            var kod = DataService.AddRepairs(Repairs.ToList());
            if (kod != -1)
            {
                await DataService.ChangeReviewStatusAndSetRefToRepairByKod(Review.Id, ReviewStatus.SetLink, kod);
                Process.Start(CheckManager.CreateRepairCheck(DataService.GetRepairsByKod(kod)));
            }

        });


    }
}