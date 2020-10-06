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

        public Color Color
        {
            get
            {
                return Color.FromRgb(ttb0.Value, ttb1.Value, ttb2.Value);
            }
            set
            {
                ttb0.Text = value.R.ToString();
                ttb1.Text = value.G.ToString();
                ttb2.Text = value.B.ToString();
                RecColorElem.Fill = new SolidColorBrush(Color);
                OnColorChanged();
            }
        }

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
            ttb0.ColorTypeInput = ttb1.ColorTypeInput = ttb2.ColorTypeInput = InputType.Dec;
        }

        private void RadioButtonHex_Checked(object sender, RoutedEventArgs e)
        {
            ttb0.ColorTypeInput = ttb1.ColorTypeInput = ttb2.ColorTypeInput = InputType.Hex;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            RecColorElem.Fill = new SolidColorBrush(Color);
            OnColorChanged();
        }

    }
}
