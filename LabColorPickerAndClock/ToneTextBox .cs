using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Converters;
using System.Windows.Input;

namespace LabColorPickerAndClock
{
    public class ToneTextBox : TextBox
    {
        private const int MAX_COLOR_VAL = 255;
        private const int MIN_COLOR_VAL = 0;
        private const string AllovedSymbolsHex = "0123456789abcdef";
        private InputType _inputType = InputType.Dec;

        public InputType ColorTypeInput
        {
            get { return _inputType; }
            set
            {
                if (value != _inputType)
                {
                    if (value == InputType.Dec)
                    {
                        _inputType = InputType.Dec;
                        this.Text = int.Parse(this.Text, NumberStyles.AllowHexSpecifier | NumberStyles.HexNumber).ToString();
                    }
                    else
                    {
                        _inputType = InputType.Hex;
                        this.Text = Convert.ToString(int.Parse(this.Text), 16);
                    }
                }
            }
        }

        public uint Value
        {
            get { return uint.Parse(this.Text); }
            set
            {
                if (value > MAX_COLOR_VAL)
                {
                    this.Text = _inputType == InputType.Dec ? MAX_COLOR_VAL.ToString() : Convert.ToString(MAX_COLOR_VAL, 16);
                }
                else
                {
                    if (_inputType == InputType.Dec)
                    {
                        this.Text = value.ToString();
                    }
                    else
                    {
                        this.Text = Convert.ToString(value, 16);
                    }
                }
            }
        }

        public ToneTextBox()
        {
            this.Text = "0";
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            for (int i = 0; i < this.Text.Length; i++)
            {
                if (!IsCorrectDigit(this.Text[i]))
                {
                    this.Text = this.Text.Substring(0, i);
                    break;
                }
            }

            int x;
            if (!NumberTryParse(this.Text, out x) || x > MAX_COLOR_VAL)
            {
                if (this.Text.Length > 0)
                {
                    this.Text = _inputType == InputType.Dec ? MAX_COLOR_VAL.ToString() : Convert.ToString(MAX_COLOR_VAL, 16);
                }
                else
                {
                    this.Text = MIN_COLOR_VAL.ToString();
                }
            }
            base.OnTextChanged(e);
        }

        private bool IsCorrectDigit(char ch)
        {
            if (this._inputType == InputType.Dec)
            {
                return char.IsDigit(ch);
            }
            else
            {
                return AllovedSymbolsHex.Contains(ch);
            }
        }

        private bool NumberTryParse(string str, out int x)
        {
            if (this._inputType == InputType.Dec)
            {
                return int.TryParse(str, out x);
            }
            else
            {
                return int.TryParse(str, NumberStyles.AllowHexSpecifier | NumberStyles.HexNumber, CultureInfo.InvariantCulture, out x);
            }
        }
    }
}
