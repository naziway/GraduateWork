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
        public static Reviews Convert(this Review review, int id, int kod)
        {
            return new Reviews
            {
                Id = id,
                Kod = kod,
                Status = (int)review.Status,
                OrderDate = review.OrderDate,
                UserId = review.User.Id,
                DeviceId = review.Device.Id,
                WorkerId = review.Worker.Id,
                RepairId = review?.Repair?.Id//Warning//TODO
            };
        }
        public static Repairs Convert(this Repair repair, int id, int kod)
        {
            return new Repairs
            {
                Id = id,
                Kod = kod,
                Status = (int)repair.Status,
                OrderDate = repair.OrderDate,
                IsWarranty = repair.IsWarranty,
                RepairDeviceId = repair.RepairDevice.Id,
                DeviceId = repair.Device.Id,
                WorkerId = repair.Worker.Id,
                PartId = repair?.Part.Id,//Warning//TODO
                WorkId = repair.Work.Id
            };
        }

    }
}