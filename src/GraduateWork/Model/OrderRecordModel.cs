﻿using PropertyChanged;
using System;

namespace Model
{
    [ImplementPropertyChanged]
    public class OrderRecordModel
    {
        public int Id { get; set; }
        public int OrderKod { get; set; }

        public DateTime StartData { get; set; }
        public DateTime FinishData { get; set; }
        public double Price { get; set; }

        public Client Client { get; set; }
        public Device Device { get; set; }

    }
}