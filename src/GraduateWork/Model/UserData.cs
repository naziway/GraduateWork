using PropertyChanged;

namespace Model
{
    [ImplementPropertyChanged]
    public class UserData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasportData { get; set; }
        public System.DateTime BirthDate { get; set; }
    }
}