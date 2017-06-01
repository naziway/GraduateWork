using DatabaseService;
using Model;
using PropertyChanged;
using Shared;
using System.Windows.Input;

namespace ViewModel.ResourseAdd
{
    [ImplementPropertyChanged]
    public class NewClientViewModel
    {
        public DataService Service { get; set; }

        public NewClientViewModel(DatabaseService.DataService service)
        {
            Service = service;
        }

        public Client Client { get; set; } = new Client();
        public ICommand AddClientCommand => new CommandHandler(() =>
        {
            Service.AddClient(Client);
        });
        public ICommand ClearCommand => new CommandHandler(() =>
        {
            Client = new Client();
        });
    }
}