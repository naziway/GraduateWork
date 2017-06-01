using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Charts;

namespace PieChartDemo.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView
    {
        private const int ClickDelta = 200;
        private DateTime _mouseDownTime;
        private bool _rotate;
        private Point _startPosition;

        public MainView()
        {
            InitializeComponent();
        }

        private bool IsClick(DateTime mouseUpTime)
        {
            return (mouseUpTime - _mouseDownTime).TotalMilliseconds < ClickDelta;
        }

        private void chart_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var hitInfo = chart.CalcHitInfo(e.GetPosition(chart));
            _rotate = false;

            if (hitInfo == null || hitInfo.SeriesPoint == null || !IsClick(DateTime.Now))
                return;

            var distance = PieSeries.GetExplodedDistance(hitInfo.SeriesPoint);
            var storyBoard = new Storyboard();
            var animation = new DoubleAnimation();

            animation.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 300));
            animation.To = distance > 0 ? 0 : 0.3;
            storyBoard.Children.Add(animation);
            Storyboard.SetTarget(animation, hitInfo.SeriesPoint);
            Storyboard.SetTargetProperty(animation, new PropertyPath(PieSeries.ExplodedDistanceProperty));
            storyBoard.Begin();
        }

        private void chart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseDownTime = DateTime.Now;
            var position = e.GetPosition(chart);
            var hitInfo = chart.CalcHitInfo(position);

            if (hitInfo != null && hitInfo.SeriesPoint != null)
            {
                _rotate = true;
                _startPosition = position;
            }
        }
    }
}
