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

namespace ViewModel.ResourseAdd
{
    [ImplementPropertyChanged]
    public class NewReviewViewModel
    {
        public DataService DataService { get; set; }
        public CheckManager CheckManager { get; set; }

        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<Device> Devices { get; set; }
        public ObservableCollection<User> Workers { get; set; }
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
        public User SelectedWorker { get; set; }

        private void DoOnSelectedDevice()
        {
            CanExecute = true;
        }
        private void DoOnSelectedClient()
        {
            Devices = new ObservableCollection<Device>
                (DataService.GetDevicesByClientId(selectedClient.Id));
            CanExecute = false;
            if (Devices.Count > 0)
                SelectedDevice = Devices.First();

        }

        public ICommand AddReviewCommand => new CommandHandler((() =>
          {
              var review = new Review
              {
                  Worker = SelectedWorker,
                  Device = SelectedDevice,
                  OrderDate = DateTime.Now,
                  Status = ReviewStatus.New
              };

              var addedReview = DataService.AddReview(review);

              if (addedReview != null)
                  Process.Start(CheckManager.CreateReviewCheck(addedReview));


          }), CanExecute);

        public string StatusMessage { get; set; }

        public bool CanExecute { get; set; }

        public NewReviewViewModel(DataService dataService)
        {
            DataService = dataService;
            CheckManager = new CheckManager(dataService);
            Clients = new ObservableCollection<Client>(DataService.GetClients());

            Workers = new ObservableCollection<User>
               (DataService.GetUsers().Where(user => user.UserType == UserType.Worker));
            if (Workers.Count > 0)
                SelectedWorker = Workers.First();
        }
    }
}