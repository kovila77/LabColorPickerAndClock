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
        /// UTC offset
        /// </summary>
        public TimeSpan UtcOffset { get; set; } = DateTimeOffset.Now.Offset;


        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        private RotateTransform _rotateTransformSecond = new RotateTransform(0);
        private RotateTransform _rotateTransformMinute = new RotateTransform(0);
        private RotateTransform _rotateTransformHour = new RotateTransform(0);

        public CustomClock2()
        {
            InitializeComponent();

            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Start();

            _rotateTransformSecond.CenterX = _rotateTransformMinute.CenterX = _rotateTransformHour.CenterX = 50;
            _rotateTransformSecond.CenterY = _rotateTransformMinute.CenterY = _rotateTransformHour.CenterY = 50;
        }

        ~CustomClock2()
        {
            _dispatcherTimer.Tick -= _dispatcherTimer_Tick;
        }

        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTimeOffset.UtcNow.DateTime.Add(UtcOffset);

            // Computing angles
            _rotateTransformSecond.Angle = dateTime.Second * 6;
            _rotateTransformMinute.Angle = (dateTime.Minute + dateTime.Second / 60f) * 6f;
            _rotateTransformHour.Angle = (dateTime.Hour + (dateTime.Minute + dateTime.Second / 60f) / 60f) * 30f;

            // Rotate hands
            MinuteHand.RenderTransform = _rotateTransformMinute;
            SecondHand.RenderTransform = _rotateTransformSecond;
            HourHand.RenderTransform = _rotateTransformHour;
        }
    }
}
