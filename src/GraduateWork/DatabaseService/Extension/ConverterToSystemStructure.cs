using Model;

namespace DatabaseService.Extension
{
    public class ConverterToSystemStructure
    {
        public Client ToClient(Clients clients)
        {
            return new Client();
        }
        public Device ToDevice(Devices devices)
        {
            return new Device();
        }
        public RepairDevice ToDevice(RepairDevices devices)
        {
            return new RepairDevice();
        }
        public Part ToPart(Parts parts)
        {
            return new Part();
        }
        public Work Convert(Works works)
        {
            return new Work();
        }
        public User Convert(Users users)
        {
            return new User();
        }
        public UserData Convert(PersonalData personalData)
        {
            return new UserData();
        }
    }
}