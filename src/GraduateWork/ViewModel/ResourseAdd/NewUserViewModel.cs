using DatabaseService;
using Model;
using PropertyChanged;
using Shared;
using Shared.Enum;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel.ResourseAdd
{
    [ImplementPropertyChanged]
    public class NewUserViewModel
    {
        public DataService Service { get; set; }

        public ObservableCollection<UserType> Users => new ObservableCollection<UserType>
        {
            UserType.Адміністратор,
            UserType.Manager,
            UserType.Worker
        };

        public NewUserViewModel(DatabaseService.DataService service)
        {
            Service = service;
        }
        public User User { get; set; }

        public ICommand AddUserCommand => new CommandHandler((() =>
          {
              User = new User();
          }));
        public ICommand ClearCommand => new CommandHandler(() =>
        {
            Service.AddUser(User);
        });
    }
}