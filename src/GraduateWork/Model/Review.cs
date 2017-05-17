using PropertyChanged;
using Shared.Enum;
using System;

namespace Model
{
    [ImplementPropertyChanged]
    public class Review
    {
        public int Id { get; set; }
        public int Kod { get; set; }
        public DateTime OrderDate { get; set; }
        public ReviewStatus Status { get; set; }
        public User User { get; set; }
        public User Worker { get; set; }
        public Device Device { get; set; }
        public Repair Repair { get; set; }
    }
}