using Model;

namespace DatabaseService.Extension
{
    public static class ExtensionToBdStructure
    {
        public static Users Convert(this Userr user)
        {
            return new Users();
        }

        public static Devices Convert(this Devicee device)
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

        public static Sellings Convert(this Selling selling, int id, int kod)
        {
            return new Sellings
            {
                Id = id,
                ClientId = selling.Client.Id,
                Kod = kod,
                PartId = selling.Part.Id,
                Status = (int)selling.Status,
                OrderDate = selling.OrderDate,
                UserId = selling.User.Id
            };
        }

    }
}