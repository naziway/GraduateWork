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
    public class NewExaminateViewModel
    {
        public DataService DataService { get; set; }

        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<Device> Devices { get; set; }

        public Device selectedDevice { get; set; }

        public Client selectedClient { get; set; }

        public Device SelectedDevice
        {
            get { return selectedDevice; }
            set
            {
                selectedDevice = value;
                DoOnSelectedDevice();
            }
        }

        public Client SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                DoOnSelectedClient();
            }
        }

        private void DoOnSelectedDevice()
        {
            CanExecute = true;
        }

        private void DoOnSelectedClient()
        {
            Devices = new ObservableCollection<Device>
                (DataService.GetDevicesByClientId(selectedClient.Id));
            CanExecute = false;
        }

        public ICommand AddExaminateCommand => new CommandHandler((() =>
          {
              var device = new Device();

              device = selectedDevice;
              DataService.AddNewExaminate(device);

          }), CanExecute);

        public bool CanExecute { get; set; }

        public NewExaminateViewModel(DataService dataService)
        {
            DataService = dataService;
            Clients = new ObservableCollection<Client>(DataService.GetClientsList());
            if (Clients.Count > 0)
                selectedClient = Clients.First();
        }
    }
}