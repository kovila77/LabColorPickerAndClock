using LabColorPickerAndClock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

namespace TestControlApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            someRectangle.Fill = new SolidColorBrush(colorPicker.Color);
        }

        private void colorPicker_ColorChanged(object sender, ColorChangedEventArgs e)
        {            
            if (someRectangle != null)
                someRectangle.Fill = new SolidColorBrush(e.Color); // If color of colorPicker changed, then fill the random rectangle into new color
        }
    }
}
