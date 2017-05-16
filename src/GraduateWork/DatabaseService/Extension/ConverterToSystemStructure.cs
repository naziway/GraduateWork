using Model;
using Shared;
using Shared.Enum;

namespace DatabaseService.Extension
{
    public class ConverterToSystemStructure
    {
        public Client ToClient(Clients clients)
        {
            return new Client
            {
                Id = clients.Id,
                Name = clients.LastName,
                Surname = clients.FirstName,
                Phone = clients.PhoneNumber,
                PassportData = clients.PasportData
            };
        }
        public Device ToDevice(Devices devices)
        {
            return new Device();
        }
        public RepairDevice ToDevice(RepairDevices device)
        {
            return new RepairDevice
            {
                Id = device.Id,
                Model = device.Model,
                DeviceType = (DeviceType)device.DeviceType,
                SerialNumber = device.SerialNumber,
                ManufactureDate = device.ManufactureDate,
                Marka = device.Marka,
                Count = device.Count
            };
        }
        public Part ToPart(Parts parts)
        {
            return new Part
            {
                Id = parts.Id,
                Title = parts.Title,
                Count = parts.Count,
                Model = parts.Model,
                Price = parts.Price,
                Marka = parts.Marka
            };
        }
        public Work Convert(Works work)
        {
            return new Work
            {
                Id = work.Id,
                Title = work.Title,
                Price = work.Price,
                Time = work.Time
            };
        }
        public Userr Convert(Users users)
        {
            return new Userr
            {
                Id = users.Id,
                Login = users.Login,
                Password = users.Password,
                RegistrationDate = users.RegistrationDate,
                UserType = (UserType)users.UserType
            };
        }
        public UserData Convert(PersonalData personalData)
        {
            return new UserData
            {
                Id = personalData.Id,
                FirstName = personalData.FirstName,
                LastName = personalData.LastName,
                PasportData = personalData.PasportData,
                BirthDate = personalData.BirthDate
            };
        }
    }
}