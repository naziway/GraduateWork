using PropertyChanged;
using Shared.Enum;
using System;

namespace Model
{
    [ImplementPropertyChanged]
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public UserType UserType { get; set; }
        public UserData UserData { get; set; }
        public double Salary { get; set; }
    }
}
