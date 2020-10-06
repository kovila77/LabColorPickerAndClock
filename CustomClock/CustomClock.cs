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
        static CustomClock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomClock), new FrameworkPropertyMetadata(typeof(CustomClock)));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Point center = new Point(this.Width / 2, this.Height / 2);
            double radius = Math.Min(this.Width, this.Height);

            drawingContext.DrawEllipse(
                this.Background,
                new Pen(this.BorderBrush, radius*0.02),
                center,
                radius,
                radius);

            double angle = 0;
            for (int i = 0; i < 12; i++, angle += Math.PI / 6)
            {
                drawingContext.DrawLine(
                    new Pen(this.BorderBrush, radius * 0.02),
                    new Point(center.X + radius * (Math.Cos(angle)) - radius * 0.10 * (Math.Cos(angle))
                            , center.Y + radius * (Math.Sin(angle)) - radius * 0.10 * (Math.Sin(angle))),
                    new Point(center.X + radius * (Math.Cos(angle)) - radius * 0.30 * (Math.Cos(angle))
                            , center.Y + radius * (Math.Sin(angle)) - radius * 0.30 * (Math.Sin(angle))));
            }

            angle = 0;
            for (int i = 0; i < 12; i++)
            {
                angle += Math.PI / 30;
                for (int j = 0; j < 4; j++, angle+=Math.PI/30)
                {
                    drawingContext.DrawLine(
                        new Pen(this.BorderBrush, radius * 0.01),
                        new Point(center.X + radius * (Math.Cos(angle)) - radius * 0.10 * (Math.Cos(angle))
                                , center.Y + radius * (Math.Sin(angle)) - radius * 0.10 * (Math.Sin(angle))),
                        new Point(center.X + radius * (Math.Cos(angle)) - radius * 0.20 * (Math.Cos(angle))
                                , center.Y + radius * (Math.Sin(angle)) - radius * 0.20 * (Math.Sin(angle))));
                }
            }




            base.OnRender(drawingContext);
        }
    }
}
