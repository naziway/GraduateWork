using PropertyChanged;
using System.Collections.Generic;

namespace Model
{
    [ImplementPropertyChanged]
    public class Devices
    {
        public List<Device> ListDevice { get; set; }

        public Devices(List<Device> listDevice)
        {
            ListDevice = listDevice;
        }

        public override string ToString()
        {
            string str = "";
            ListDevice.ForEach(device => { str += $"[{device.PhoneMarka}-{device.PhoneModel}]"; });
            return str;
        }
    }
}