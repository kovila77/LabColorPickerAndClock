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
        private const string AllovedSymbolsHex = "0123456789abcdefABCDEF";
        private InputType _inputType = InputType.Dec;

        /// <summary>
        /// Current type of input values
        /// </summary>
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
                        this.Text = byte.Parse(this.Text, NumberStyles.HexNumber).ToString();
                    }
                    else
                    {
                        _inputType = InputType.Hex;
                        this.Text = Convert.ToString(byte.Parse(this.Text), 16);
                    }
                }
            }
        }

        /// <summary>
        /// Tone value in byte (0 .. 255)
        /// </summary>
        public byte Value
        {
            get { return _inputType == InputType.Dec ? byte.Parse(this.Text) : byte.Parse(this.Text,  NumberStyles.HexNumber); }
            set
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

        public ToneTextBox()
        {
            this.Text = byte.MinValue.ToString();
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

            byte x;
            if (!NumberTryParse(this.Text, out x))
            {
                if (this.Text.Length > 0)
                {
                    this.Text = _inputType == InputType.Dec ? byte.MaxValue.ToString() : Convert.ToString(byte.MaxValue, 16);
                }
                else
                {
                    this.Text = byte.MinValue.ToString();
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

        private bool NumberTryParse(string str, out byte x)
        {
            if (this._inputType == InputType.Dec)
            {
                return byte.TryParse(str, out x);
            }
            else
            {
                return byte.TryParse(str, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out x);
            }
        }
    }
}
