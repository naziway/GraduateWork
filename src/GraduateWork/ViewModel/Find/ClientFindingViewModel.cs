using DatabaseService;
using Model;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ViewModel.Base;
using ViewModel.Lists;

namespace ViewModel.Find
{
    [ImplementPropertyChanged]
    public class ClientFindingViewModel : FindViewModel
    {
        private DataService DataService { get; set; }
        public ClientFindingViewModel(DataService dataService)
        {
            DataService = dataService;

            Clients = new ClientListViewModel(dataService);
            FindingParametersList = new List<string>()
            {   "Все",
                "Ім'я",
                "Прізвище"
            };
            SelectedParam = FindingParametersList.First();
        }

        public ClientListViewModel Clients { get; set; }

        protected override void Finding(string text)
        {
            List<Client> findClients;
            switch (SelectedParam)
            {
                case "Все":
                    findClients = DataService.GetClients().Where(client =>
                        client.FirstName.ToLower().Contains(text.ToLower()) ||
                        client.LastName.ToLower().Contains(text.ToLower()) ||
                        client.PhoneNumber.ToLower().Contains(text.ToLower()) ||
                        client.PasportData.ToLower().Contains(text.ToLower())).ToList();
                    break;
                case "Ім'я":
                    findClients = DataService.GetClients().Where(client =>
                        client.LastName.ToLower().Contains(text.ToLower())).ToList();
                    break;
                case "Прізвище":
                    findClients = DataService.GetClients().Where(client =>
                        client.FirstName.ToLower().Contains(text.ToLower())).ToList();
                    break;
                default:
                    return;
            }
            InvokeInMainThread(() => { Clients.Clients = new ObservableCollection<Client>(findClients); });

        }
        private void InvokeInMainThread(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
    }
}