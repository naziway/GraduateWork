using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace GraduateWork.Base
{
    /// <summary>
    /// Interaction logic for BaseView.xaml
    /// </summary>
    public partial class BaseView : Window
    {
        public BaseView()
        {
            InitializeComponent();
        }
        public BaseView(UserControl userControl, string title, int height, int width) : this()
        {
            Title = title;
            Height = height;
            Width = width;
            TitleView.Content = title;
            MainContaint.Content = userControl;
        }
        private void MoveOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
