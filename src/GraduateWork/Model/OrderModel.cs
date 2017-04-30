using PropertyChanged;
using Shared.Enum;
using System;
using System.Collections.Generic;

namespace Model
{
    [ImplementPropertyChanged]
    public class OrderModel
    {
        public OrderModel()
        {
            Works = new List<WorkModel>();
            Parts = new List<Part>();
        }

        public int OrderKod { get; set; }
        public DateTime StartData { get; set; }
        public DateTime FinishData { get; set; }
        public double Price { get; set; }
        public Device Device { get; set; }
        public Device RepairDevice { get; set; }
        public OrderType OrderType { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<WorkModel> Works { get; set; }
        public List<Part> Parts { get; set; }
    }
}