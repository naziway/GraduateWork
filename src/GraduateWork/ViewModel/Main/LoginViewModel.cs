using DatabaseService;
using Model;
using PropertyChanged;
using Shared;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class LoginViewModel
    {
        private readonly DataService service;

        public LoginViewModel()
        {
            service = new DataService();
            Login = "naziway";
            Password = "1";
        }

        public string Login { get; set; }
        public Visibility State { get; set; } = Visibility.Hidden;
        public string Password { get; set; }

        public ICommand LoginCommand => new CommandHandler(() => { Task.Factory.StartNew(CheckLoginAndPassword); });

        public void CheckLoginAndPassword()
        {
            State = Visibility.Visible;
            var user = service.GetUser(Login, Password);
            if (user == null)
            {
                DoOnFailedLogin();
                State = Visibility.Hidden;
                return;
            }
            DoOnSuccessLogin(user);
        }

        public event EventHandler<User> OnSuccessLogin;
        public event EventHandler OnFailedLogin;
        private void DoOnSuccessLogin(User e)
        {
            OnSuccessLogin?.Invoke(this, e);
        }
        private void DoOnFailedLogin()
        {
            OnFailedLogin?.Invoke(this, EventArgs.Empty);
        }
    }
}