using DatabaseService;
using Model;
using PropertyChanged;
using Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ViewModel.ResourseAdd
{
    [ImplementPropertyChanged]
    public class NewDeviceViewModel
    {
        public DataService Service { get; set; }
        public Device Device { get; set; } = new Device();

        public ObservableCollection<Client> Clients { get; set; }

        public ObservableCollection<DeviceType> DeviceTypes { get; set; } = new ObservableCollection<DeviceType>
        {
            DeviceType.Телефон,
            DeviceType.Планшет
        };
        public NewDeviceViewModel(DatabaseService.DataService service)
        {
            Service = service;
            Clients = new ObservableCollection<Client>(Service.GetClients());
            if (Clients.Any())
                Device.Client = Clients.First();
            Device.DeviceType = DeviceTypes.First();
        }
        public ICommand AddClientCommand => new CommandHandler(() =>
        {
            Service.AddDevice(Device);
        });
        public ICommand ClearCommand => new CommandHandler(() =>
        {
            Device = new Device();
            if (Clients.Any())
                Device.Client = Clients.First();
            Device.DeviceType = DeviceTypes.First();
        });
    }
}