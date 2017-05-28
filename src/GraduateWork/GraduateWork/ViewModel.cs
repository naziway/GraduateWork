using Model;
using System.Collections.Generic;

namespace GraduateWork
{
    public class ViewModel
    {
        public List<HistogramModel> Items { get; set; }
        public ViewModel()
        {
            Items = new List<HistogramModel>
            {

                new HistogramModel {Argument = "1",ReviewValue = 15555,RepairValue = 1458,SellingValue = 14885},
                new HistogramModel {Argument = "2",ReviewValue = 15455,RepairValue = 1458,SellingValue = 14885},
                new HistogramModel {Argument = "3",ReviewValue = 15455,RepairValue = 458,SellingValue = 14685},
                new HistogramModel {Argument = "4",ReviewValue = 15555,RepairValue = 1458,SellingValue = 14885},
                new HistogramModel {Argument = "5",ReviewValue = 66555,RepairValue = 1458,SellingValue = 1385},
                new HistogramModel {Argument = "6",ReviewValue = 66555,RepairValue = 1458,SellingValue = 1385},

            };
        }
    }
}