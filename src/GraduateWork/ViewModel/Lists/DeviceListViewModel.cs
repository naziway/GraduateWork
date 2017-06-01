using DatabaseService;
using Model;
using System.Collections.ObjectModel;

namespace ViewModel.Lists
{
    public class DeviceListViewModel
    {
        public DataService DataService { get; set; }

        public DeviceListViewModel(DataService dataService)
        {
            DataService = dataService;

            Devices = new ObservableCollection<Device>(DataService.GetDevices());
        }
        public ObservableCollection<Device> Devices { get; set; }
    }
}