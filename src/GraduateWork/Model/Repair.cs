using PropertyChanged;
using Shared.Enum;
using System;

namespace Model
{
    [ImplementPropertyChanged]
    public class Repair
    {
        public int Id { get; set; }
        public int Kod { get; set; }
        public DateTime OrderDate { get; set; }
        public RepairStatus Status { get; set; }
        public bool IsWarranty { get; set; }
        public RepairDevice RepairDevice { get; set; }//change chem
        public User Worker { get; set; }
        public Device Device { get; set; }
        public Part Part { get; set; }
        public Work Work { get; set; }

    }
}