using System.Windows.Controls;
using System.Windows.Input;

namespace UserControls
{
    /// <summary>
    /// Interaction logic for PartList.xaml
    /// </summary>
    public partial class ListPart : UserControl
    {
        public ListPart()
        {
            InitializeComponent();
        }

        public ICommand Command { get; set; }
    }
}
