using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CustomClock
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CustomClock"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CustomClock;assembly=CustomClock"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class CustomClock : Control
    {
        public static readonly DependencyProperty StrokeColorProperty;

        public Brush StrokeColor { get; set; }

        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        static CustomClock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomClock), new FrameworkPropertyMetadata(typeof(CustomClock)));
            StrokeColorProperty =
                DependencyProperty.Register(
                    "StrokeColor",
                    typeof(Brush), typeof(CustomClock),
                    new PropertyMetadata(
                        new LinearGradientBrush(Color.FromRgb(255, 255, 255), Color.FromRgb(0xc8, 0xc8, 0xc8), 0),
                        new PropertyChangedCallback((sender, args) => { ((CustomClock)sender).StrokeColor = (Brush)args.NewValue; })
                        )
                    );
        }

        public CustomClock()
        {
            StrokeColor = (Brush)StrokeColorProperty.DefaultMetadata.DefaultValue;
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Start();
            line.X1 = 0;
            line.X2 = 0;
            line.Y1 = 50;
            line.Y2 = 50;
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 10;

            myGrid.Children.Add(line);
            myGrid.Width = 100;
            myGrid.Height= 100;
            this.AddVisualChild(myGrid);
        }

        //DrawingVisual drawingVisual = new DrawingVisual();
        Line line = new Line();
        Grid myGrid = new Grid();

        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //MessageBox.Show("sdfasdf");
            //RotateTransform rotateTransform = new RotateTransform(30);
            //var dc = drawingVisual.RenderOpen();
            //dc.PushTransform(rotateTransform);
            ////drawingVisual.Transform = rotateTransform;
            //dc.Close();

            //var t = this.LogicalChildren;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Point center = new Point(this.ActualWidth / 2, this.ActualHeight / 2);
            double radius = Math.Min(this.ActualWidth, this.ActualHeight) / 2;

            // support circle 
            drawingContext.PushTransform(new RotateTransform(45, center.X, center.Y));
            drawingContext.DrawEllipse(
                StrokeColor,
                new Pen(this.BorderBrush, radius * 0.02),
                center,
                radius,
                radius);
            drawingContext.Pop();

            // Main circle
            drawingContext.DrawEllipse(
                this.Background,
                new Pen(this.BorderBrush, radius * 0.02),
                center,
                radius * 0.96,
                radius * 0.96);

            //double angle = 0;
            //for (int i = 1; i <= 12; i++, angle += Math.PI / 6)
            //{
            //    drawingContext.DrawLine(
            //        new Pen(this.BorderBrush, radius * 0.02),
            //        new Point(center.X + radius * (Math.Cos(angle)) - radius * 0.10 * (Math.Cos(angle))
            //                , center.Y + radius * (Math.Sin(angle)) - radius * 0.10 * (Math.Sin(angle))),
            //        new Point(center.X + radius * (Math.Cos(angle)) - radius * 0.30 * (Math.Cos(angle))
            //                , center.Y + radius * (Math.Sin(angle)) - radius * 0.30 * (Math.Sin(angle))));
            //}

            //angle = 0;
            //for (int i = 0; i < 12; i++)
            //{
            //    angle += Math.PI / 30;
            //    for (int j = 0; j < 4; j++, angle += Math.PI / 30)
            //    {
            //        drawingContext.DrawLine(
            //            new Pen(this.BorderBrush, radius * 0.01),
            //            new Point(center.X + radius * (Math.Cos(angle)) - radius * 0.10 * (Math.Cos(angle))
            //                    , center.Y + radius * (Math.Sin(angle)) - radius * 0.10 * (Math.Sin(angle))),
            //            new Point(center.X + radius * (Math.Cos(angle)) - radius * 0.20 * (Math.Cos(angle))
            //                    , center.Y + radius * (Math.Sin(angle)) - radius * 0.20 * (Math.Sin(angle))));
            //    }
            //}

            // Small point around
            var p = new Pen(this.BorderBrush, radius * 0.02);
            var ds = DashStyles.Dash.Clone();
            ds.Dashes[0] = 1;
            ds.Dashes[1] = 3.658;
            p.DashStyle = ds;
            p.DashCap = 0;

            drawingContext.PushTransform(new RotateTransform(0.57, center.X, center.Y));
            drawingContext.DrawEllipse(
                null,
                p,
                center,
                radius * 0.889,
                radius * 0.889);
            drawingContext.Pop();

            // Big line around
            var p1 = new Pen(this.BorderBrush, radius * 0.2);
            var ds1 = DashStyles.Dash.Clone();
            ds1.Dashes[0] = 0.1;
            ds1.Dashes[1] = 1.995;
            p1.DashStyle = ds1;
            p1.DashCap = 0;

            drawingContext.PushTransform(new RotateTransform(-16.4, center.X, center.Y));
            drawingContext.DrawEllipse(
                null,
                p1,
                center,
                radius * 0.80,
                radius * 0.80);
            drawingContext.Pop();

            //drawingVisual = new DrawingVisual();
            //DrawingContext dc = drawingVisual.RenderOpen();
            //Rect rect = new Rect(new Point(0, 0), new Size(100, 100));
            //dc.DrawRectangle(Brushes.Black, null, rect);
            //dc.Close();
            //drawingContext.DrawDrawing(drawingVisual.Drawing);
            //drawingContext.DrawDrawing(new DrawingVisual());

            //this.AddVisualChild(line);


            // small point in center
            drawingContext.DrawEllipse(this.BorderBrush,
                    new Pen(this.BorderBrush, radius * 0.02),
                    center,
                    radius * 0.02,
                    radius * 0.02);


            base.OnRender(drawingContext);
        }

    }
}
