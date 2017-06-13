using Model;
using System;

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
            return new Devices()
            {
                DeviceType = (int)device.DeviceType,
                ClientId = device.Client.Id,
                Id = device.Id,
                ManufactureDate = device.ManufactureDate,
                Marka = device.Marka,
                Model = device.Model,
                SerialNumber = device.SerialNumber
            };
        }
        public static RepairDevices Convert(this RepairDevice device)
        {
            return new RepairDevices();
        }

        public static Clients Convert(this Client client)
        {
            return new Clients
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PasportData = client.PasportData,
                PhoneNumber = client.PhoneNumber,
                RegistrationDate = DateTime.Now
            };
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
                OrderDate = DateTime.Now,
                UserId = selling.User.Id,Count = selling.Count
            };
        }
        public static Table Convert(this Paid paid)
        {
            return new Table
            {
                Id = paid.Id,
                DatePaid = paid.DatePaid,
                Salary = paid.Salary,
                UserId = paid.Id
            };
        }
        public static Reviews Convert(this Review review, int id, int kod)
        {
            return new Reviews
            {
                Id = id,
                Kod = kod,
                Status = (int)review.Status,
                OrderDate = DateTime.Now,
                UserId = review.User.Id,
                DeviceId = review.Device.Id,
                WorkerId = review.Worker.Id,
                RepairKod = review?.Repair?.Kod//Warning//TODO
            };
        }
        public static Repairs Convert(this Repair repair, int id, int kod)
        {
            return new Repairs
            {
                Id = id,
                Kod = kod,
                Status = (int)repair.Status,
                OrderDate = DateTime.Now,
                IsWarranty = repair.IsWarranty,
                RepairDeviceId = repair?.RepairDevice?.Id,
                DeviceId = repair.Device.Id,
                WorkerId = repair.Worker.Id,
                PartId = repair?.Part?.Id,//Warning//TODO
                WorkId = repair.Work.Id
            };
        }

    }
}