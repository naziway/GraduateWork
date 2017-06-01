using System.Collections.Generic;

namespace PieChartDemo.ViewModels
{
    public class MainViewModel
    {
        public virtual string Title { get; set; }
        public virtual IEnumerable<PieChartItemVM> Items { get; set; }

        public MainViewModel()
        {
            Title = "Pie Chart Example";

            Items = new PieChartItemVM[]
            {
                new PieChartItemVM{Name = "Item1", Value = 2d}, 
                new PieChartItemVM{Name = "Item2", Value = 4d}, 
                new PieChartItemVM{Name = "Item3", Value = 6d}, 
                new PieChartItemVM{Name = "Item4", Value = 8d}, 
                new PieChartItemVM{Name = "Item5", Value = 10d}, 
            };
        }
    }
}