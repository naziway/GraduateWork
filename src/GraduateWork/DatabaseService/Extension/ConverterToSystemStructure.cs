using Model;
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
        public RepairDevice ToDevice(RepairDevices devices)
        {
            return new RepairDevice();
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
        public Work Convert(Works works)
        {
            return new Work();
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