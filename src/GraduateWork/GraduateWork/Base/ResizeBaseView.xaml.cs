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
    /// Interaction logic for ResizeBaseView.xaml
    /// </summary>
    public partial class ResizeBaseView : Window
    {
        private bool resixe;

        public ResizeBaseView()
        {
            InitializeComponent();
        }
        public ResizeBaseView(UserControl userControl, string title, int height, int width) : this()
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
        private void RightOnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            resixe = false;
            Image rect = (Image)sender;
            rect.ReleaseMouseCapture();
        }
        private void RightOnMouseMove(object sender, MouseEventArgs e)
        {
            Image rect = (Image)sender;
            if (resixe)
            {
                rect.CaptureMouse();
                double newWidth = e.GetPosition(this).X + 10;
                double newHeight = e.GetPosition(this).Y + 10;
                if (newWidth > 0) this.Width = newWidth;
                if (newHeight > 0) this.Height = newHeight;
            }
        }
        private void RightOnLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            resixe = true;
        }
        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Max(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }
        private void Normal(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Normal;
        }
        private void Min(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

    }
}
