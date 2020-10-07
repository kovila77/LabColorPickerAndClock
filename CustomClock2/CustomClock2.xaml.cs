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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CustomClock2
{
    /// <summary>
    /// Interaction logic for CustomClock2.xaml
    /// </summary>
    public partial class CustomClock2 : UserControl
    {
        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();
        public DateTime Time { get; set; } = DateTimeOffset.UtcNow.DateTime;

        public TimeSpan UtcOffset { get; set; } = DateTimeOffset.Now.Offset;

        RotateTransform rotateTransformSecond = new RotateTransform(0);
        RotateTransform rotateTransformMinute = new RotateTransform(0);
        RotateTransform rotateTransformHour = new RotateTransform(0);

        public CustomClock2()
        {
            InitializeComponent();
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Start();
            rotateTransformSecond.CenterX = rotateTransformMinute.CenterX = rotateTransformHour.CenterX = 50;
            rotateTransformSecond.CenterY = rotateTransformMinute.CenterY = rotateTransformHour.CenterY = 50;
        }

        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //Time = Time.AddSeconds(1);
            //rotateTransformSecond.Angle = Time.Second * 6;
            //SecondHand.RenderTransform = rotateTransformSecond;

            //rotateTransformMinute.Angle = Time.Minute * 6;
            //MinuteHand.RenderTransform = rotateTransformMinute;

            //rotateTransformHour.Angle = Time.Hour % 12 * 30;
            //HourHand.RenderTransform = rotateTransformHour;

            Time = Time.AddSeconds(1);

            DateTime dateTime = Time.Add(UtcOffset);
            rotateTransformSecond.Angle = dateTime.Second * 6;
            SecondHand.RenderTransform = rotateTransformSecond;

            rotateTransformMinute.Angle = (dateTime.Minute + dateTime.Second / 60f) * 6f;
            MinuteHand.RenderTransform = rotateTransformMinute;

            rotateTransformHour.Angle = (dateTime.Hour + (dateTime.Minute + dateTime.Second / 60f) / 60f) * 30f;
            HourHand.RenderTransform = rotateTransformHour;
        }
    }
}
