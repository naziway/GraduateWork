using DatabaseService;
using Model;
using PropertyChanged;
using System.Collections.ObjectModel;

namespace ViewModel.Lists
{
    [ImplementPropertyChanged]
    public class ClientListViewModel
    {
        public DataService DataService { get; set; }

        public ClientListViewModel(DataService dataService)
        {
            DataService = dataService;

            Clients = new ObservableCollection<Client>(DataService.GetClients());
        }
        public ObservableCollection<Client> Clients { get; set; }

    }
}