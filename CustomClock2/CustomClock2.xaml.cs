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
        
        /// <summary>
        /// Time that clock have without UTC
        /// </summary>
        public DateTime Time { get; set; } = DateTimeOffset.UtcNow.DateTime;

        /// <summary>
        /// UTC offset
        /// </summary>
        public TimeSpan UtcOffset { get; set; } = DateTimeOffset.Now.Offset;

        /// <summary>
        /// True if you need update time using DateTime.Now. False if you need auto increment (without sync!)
        /// </summary>
        public bool UseEveryTimeDateTimeNow { get; set; } = true;

        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        private RotateTransform rotateTransformSecond = new RotateTransform(0);
        private RotateTransform rotateTransformMinute = new RotateTransform(0);
        private RotateTransform rotateTransformHour = new RotateTransform(0);

        public CustomClock2()
        {
            InitializeComponent();
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Start();
            rotateTransformSecond.CenterX = rotateTransformMinute.CenterX = rotateTransformHour.CenterX = 50;
            rotateTransformSecond.CenterY = rotateTransformMinute.CenterY = rotateTransformHour.CenterY = 50;
        }

        ~CustomClock2()
        {
            _dispatcherTimer.Tick -= _dispatcherTimer_Tick;
        }

        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime;
            if (UseEveryTimeDateTimeNow)
            {
                dateTime = DateTimeOffset.UtcNow.DateTime.Add(UtcOffset);
            }
            else
            {
                Time = Time.AddSeconds(1);
                dateTime = Time.Add(UtcOffset);
            }

            // Computing angles
            rotateTransformSecond.Angle = dateTime.Second * 6;
            rotateTransformMinute.Angle = (dateTime.Minute + dateTime.Second / 60f) * 6f;
            rotateTransformHour.Angle = (dateTime.Hour + (dateTime.Minute + dateTime.Second / 60f) / 60f) * 30f;

            // Rotate hands
            MinuteHand.RenderTransform = rotateTransformMinute;
            SecondHand.RenderTransform = rotateTransformSecond;
            HourHand.RenderTransform = rotateTransformHour;
        }
    }
}
