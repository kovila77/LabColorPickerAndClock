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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabColorPickerAndClock
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event ColorChangedEventHandler ColorChanged;

        /// <summary>
        /// Current color in ColorPicker
        /// </summary>
        public Color Color
        {
            get
            {
                return Color.FromRgb(ttb0.Value, ttb1.Value, ttb2.Value);
            }
            set
            {
                if (ttb0.Value != value.R || ttb1.Value != value.G || ttb2.Value != value.B)
                {
                    ttb0.Value = value.R;
                    ttb1.Value = value.G;
                    ttb2.Value = value.B;
                    RecColorElem.Fill = new SolidColorBrush(Color);
                    OnColorChanged();
                }
            }
        }

        /// <summary>
        /// Current type of input values
        /// </summary>
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
            this.Color = Color.FromRgb(0, 0, 0);
        }

        /// <summary>
        /// The event will take place when the Color change in the ColorPicker
        /// </summary>
        protected virtual void OnColorChanged()
        {
            ColorChanged?.Invoke(this, new ColorChangedEventArgs(Color));
            OnPropertyChanged("Color");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RadioButtonDec_Checked(object sender, RoutedEventArgs e)
        {
            // If changed type of input values, ToneTextBox should know it to convert current values
            ttb0.ColorTypeInput = ttb1.ColorTypeInput = ttb2.ColorTypeInput = InputType.Dec;
        }

        private void RadioButtonHex_Checked(object sender, RoutedEventArgs e)
        {
            ttb0.ColorTypeInput = ttb1.ColorTypeInput = ttb2.ColorTypeInput = InputType.Hex;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // If text in some ToneTextBox changed, the color of rectangle must be set into new value of Color
            RecColorElem.Fill = new SolidColorBrush(Color);
            OnColorChanged();
        }

    }
}
