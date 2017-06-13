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

namespace UserControls.InformationControl
{
    /// <summary>
    /// Interaction logic for RepairUserControl.xaml
    /// </summary>
    public partial class RepairUserControl : UserControl
    {
        public ObservableCollection<RepairStatus> StatusList { get; set; }

        public RepairUserControl()
        {
            InitializeComponent();
            StatusList = new ObservableCollection<RepairStatus>
                {
                    RepairStatus.Сформований,
                    RepairStatus.Новий,
                    RepairStatus.Виконується,
                    RepairStatus.Виконаний,
                    RepairStatus.Оплачений
    };
        }
    }
}
