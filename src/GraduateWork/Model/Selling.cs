using PropertyChanged;
using Shared.Enum;
using System;

namespace Model
{
    [ImplementPropertyChanged]
    public class Selling
    {
        public int Id { get; set; }
        public int Kod { get; set; }
        public DateTime OrderDate { get; set; }
        public SellingStatus Status { get; set; }
        public User User { get; set; }
        public Client Client { get; set; }
        public Part Part { get; set; }

    }
}