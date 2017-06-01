using PropertyChanged;
using Shared;

namespace Model
{
    [ImplementPropertyChanged]
    public class Device
    {
        public int Id { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public DeviceType DeviceType { get; set; }
        public string SerialNumber { get; set; }
        public System.DateTime ManufactureDate { get; set; }
        public Client Client { get; set; }
    }
}