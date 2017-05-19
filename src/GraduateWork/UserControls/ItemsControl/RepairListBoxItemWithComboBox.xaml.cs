using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Shared.Enum;

namespace UserControls.ItemsControl
{
    /// <summary>
    /// Interaction logic for RepairListBoxItemWithComboBox.xaml
    /// </summary>
    public partial class RepairListBoxItemWithComboBox : UserControl
    {

        public RepairListBoxItemWithComboBox()
        {
            InitializeComponent();
            StatusList = new ObservableCollection<RepairStatus>
                {
                    RepairStatus.New,
                    RepairStatus.InProgress,
                    RepairStatus.Done,
                    RepairStatus.Canceled,
                    RepairStatus.Paid
    };
        }

        public ObservableCollection<RepairStatus> StatusList { get; set; }
    }
}
