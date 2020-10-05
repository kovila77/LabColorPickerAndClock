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

namespace LabColorPickerAndClock
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public Color Color { get { return Color.FromRgb(ttb0.Value, ttb1.Value, ttb2.Value); } }

        public InputType InputType
        {
            get { return ttb0.ColorTypeInput; }
            set
            {
                if (value != ttb0.ColorTypeInput)
                {
                    ttb0.ColorTypeInput = ttb1.ColorTypeInput = ttb2.ColorTypeInput = value;
                }
            }
        }

        public ColorPicker()
        {
            InitializeComponent();
        }

        private void RadioButtonDec_Checked(object sender, RoutedEventArgs e)
        {
            ttb0.ColorTypeInput = ttb1.ColorTypeInput = ttb2.ColorTypeInput = InputType.Dec;
        }

        private void RadioButtonHex_Checked(object sender, RoutedEventArgs e)
        {
            ttb0.ColorTypeInput = ttb1.ColorTypeInput = ttb2.ColorTypeInput = InputType.Hex;
        }

        private void OnColorChanged(object sender, TextChangedEventArgs e)
        {
            RecColorElem.Fill = new SolidColorBrush(Color);
        }
    }
}
