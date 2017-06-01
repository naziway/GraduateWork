using PropertyChanged;

namespace Model
{
    [ImplementPropertyChanged]
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasportData { get; set; }
        public string PhoneNumber { get; set; }
        public System.DateTime RegistrationDate { get; set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}