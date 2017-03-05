using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using System.Windows.Input;
using DatabaseService;
using Model;
using PropertyChanged;
using Shared;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class LoginViewModel
    {
        private readonly DataService service;

        public LoginViewModel()
        {
            service = new DataService();
        }

        public string Login { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand => new CommandHandler(CheckLoginAndPassword);

        public void CheckLoginAndPassword()
        {
            var user = service.GetUser(Login, Password);
            if (user == null)
            {
                OnOnFailedLogin();
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
        private void OnOnFailedLogin()
        {
            OnFailedLogin?.Invoke(this, EventArgs.Empty);
        }
    }
}