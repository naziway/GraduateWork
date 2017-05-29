using System;
using System.Linq;
using System.Windows.Controls;

namespace UserControls
{
    /// <summary>
    /// Interaction logic for Time.xaml
    /// </summary>
    public partial class Clock : UserControl
    {
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();

        public Clock()
        {
            InitializeComponent();
            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
        }

        private void Timer_Click(object sender, EventArgs e)
        {
            //   DateTime d;
            //  d = DateTime.Now.ToLongTimeString()
            //  var hour = d.Hour;
            //  var minute = d.Minute;
            clock.Content = new string(new string(DateTime.Now.ToLongTimeString().Reverse().ToArray()).Remove(0, 2).Reverse().ToArray());
        }
    }
}
