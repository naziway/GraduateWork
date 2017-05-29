using PropertyChanged;
using System.Windows.Input;

namespace Model
{
    [ImplementPropertyChanged]
    public class CustomMenuItem
    {
        public string ToolTip { get; set; }
        public string Img { get; set; }
        public ICommand Command { get; set; }
    }
}