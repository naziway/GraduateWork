using System.Windows.Input;

namespace Model
{
    public class HistogramModel
    {
        public string Argument { get; set; }
        public double ReviewValue { get; set; }
        public double RepairValue { get; set; }
        public double SellingValue { get; set; }
        public ICommand Command { get; set; }
    }
}