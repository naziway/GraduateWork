using PropertyChanged;

namespace Model
{
    [ImplementPropertyChanged]
    public class Device
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public string PhoneModel { get; set; }
        public string PhoneMarka { get; set; }
        public string DeviceType { get; set; }
        public string SerialNumber { get; set; }
        public System.DateTime ManufactureDate { get; set; }
    }
}