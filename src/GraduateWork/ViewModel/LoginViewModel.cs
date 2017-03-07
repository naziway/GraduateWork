using DatabaseService;
using Model;
using PropertyChanged;
using Shared;
using System;
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
        }

        public string Login { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand => new CommandHandler(CheckLoginAndPassword);

        public void CheckLoginAndPassword()
        {
            var user = service.GetUser(Login, Password);
            if (user == null)
            {
                DoOnFailedLogin();
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