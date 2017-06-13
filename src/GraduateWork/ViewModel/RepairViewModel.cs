using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DatabaseService;
using Model;
using PropertyChanged;
using Shared.Enum;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class RepairViewModel
    {
        public DataService DataService { get; set; }
        public ObservableCollection<Repair> Repairs { get; set; }
        public ObservableCollection<RepairStatus> StatusList { get; set; }
        public Client Client { get; set; }
        public Device Device { get; set; }
        public int RepairKod { get; set; }
        public double Symma { get; set; }
        public RepairViewModel(DataService dataService, List<Repair> repairs)
        {
            if (repairs == null || repairs.Count < 1)
                return;
            DataService = dataService;
            StatusList = new ObservableCollection<RepairStatus>
                {
                    RepairStatus.Сформований,
                    RepairStatus.Новий,
                    RepairStatus.Виконується,
                    RepairStatus.Виконаний,
                    RepairStatus.Оплачений
                };
            Task.Factory.StartNew(() =>
            {
                Repairs = new ObservableCollection<Repair>(repairs);
                var firstRepair = repairs.First();
                Client = firstRepair.Device.Client;
                Device = firstRepair.Device;
                RepairKod = firstRepair.Kod;
                foreach (var repair in repairs)
                {
                    Symma += repair.Work.Price;
                    if (repair.Part != null)
                        Symma += repair.Part.Price;
                }
                
            });
        }
    }
}