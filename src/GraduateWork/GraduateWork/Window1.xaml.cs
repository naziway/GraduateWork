using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GraduateWork
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private bool left;
        private bool right;
        private bool top;
        private bool bottom;

        public Window1()
        {
            InitializeComponent();
        }

        private void LeftOnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            left = false;
            Grid rect = (Grid)sender;
            rect.ReleaseMouseCapture();
        }
        private void LeftOnMouseMove(object sender, MouseEventArgs e)
        {
            Grid rect = (Grid)sender;
            if (left)
            {
                rect.CaptureMouse();
                double newWidth = e.GetPosition(this).X + 5;
                if (newWidth > 0) this.Width = newWidth;
            }
        }
        private void LeftOnLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            left = true;
        }

        private void RightOnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            right = false;
            Grid rect = (Grid)sender;
            rect.ReleaseMouseCapture();
        }
        private void RightOnMouseMove(object sender, MouseEventArgs e)
        {
            Grid rect = (Grid)sender;
            if (right)
            {
                rect.CaptureMouse();
                double newWidth = e.GetPosition(this).X + 5;
                if (newWidth > 0) this.Width = newWidth;
            }
        }
        private void RightOnLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            right = true;
        }
        private void BottomOnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bottom = false;
            Grid rect = (Grid)sender;
            rect.ReleaseMouseCapture();
        }
        private void BottomOnMouseMove(object sender, MouseEventArgs e)
        {
            Grid rect = (Grid)sender;
            if (bottom)
            {
                rect.CaptureMouse();
                double height = e.GetPosition(this).Y + 5;
                if (height > 0) this.Height = height;
            }
        }
        private void BottomOnLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bottom = true;
        }
        private void TopOnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            top = false;
        }
        private void TopOnMouseMove(object sender, MouseEventArgs e)
        {
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            Grid rect = (Grid)sender;
            if (top)
            {
                double height = +5;

                try
                {
                    Top = Mouse.GetPosition(this).Y - 5;
                    // WindowSCR.Top = (screenHeight - this.Height) / 0x00000002;
                }
                catch
                {
                    top = false;
                }
            }
        }
        private void TopOnLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            top = true;
        }
    }
}
