using PropertyChanged;
using System.Collections.Generic;

namespace Model
{
    [ImplementPropertyChanged]
    public class Client
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string PassportData { get; set; }
        public string Phone { get; set; }
        public List<Device> Devices { get; set; }
    }
}