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
        public ObservableCollection<string> Marks { get; set; }
        public ObservableCollection<string> Models { get; set; }

        public string selectedMarka { get; set; }
        public string selectedModel { get; set; }
        public Client selectedClient { get; set; }

        public string SelectedMarka
        {
            get { return selectedMarka; }
            set
            {
                selectedMarka = value;
                DoOnSelectedMarka();
            }
        }
        public string SelectedModel
        {
            get { return selectedModel; }
            set
            {
                selectedModel = value;
                DoOnSelectedModel();
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


        private void DoOnSelectedModel()
        {

        }

        private void DoOnSelectedMarka()
        {
            Models = new ObservableCollection<string>
               (DataService.GetDevicesByClientId(selectedClient.Id).Where(device => device.PhoneMarka == SelectedMarka).Select(device => device.PhoneModel));

        }

        private void DoOnSelectedClient()
        {
            Marks = new ObservableCollection<string>
                (DataService.GetDevicesByClientId(selectedClient.Id).Select(device => device.PhoneMarka));
        }

        public ICommand AddExaminateCommand => new CommandHandler((() =>
          {

          }));


        public NewExaminateViewModel(DataService dataService)
        {
            DataService = dataService;
            Clients = new ObservableCollection<Client>(DataService.GetClientsList());
            if (Clients.Count > 0)
                selectedClient = Clients.First();
        }
    }
}