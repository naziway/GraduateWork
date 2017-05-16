using Model;

namespace DatabaseService.Extension
{
    public static class ExtensionToBdStructure
    {
        public static Users Convert(this User user)
        {
            return new Users();
        }

        public static Devices Convert(this Device device)
        {
            return new Devices();
        }
        public static RepairDevices Convert(this RepairDevice device)
        {
            return new RepairDevices();
        }

        public static Clients Convert(this Client client)
        {
            return new Clients();
        }
        public static PersonalData Convert(this UserData userData)
        {
            return new PersonalData();
        }
        public static Parts Convert(this Part part)
        {
            return new Parts();
        }
        public static Works Convert(this Work work)
        {
            return new Works();
        }

    }
}