using System;
using System.Collections.Generic;
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
        public enum InputType
        {
            Dec,
            Hex,
        }

        public InputType ColorTypeInput { get; set; }

        public ToneTextBox()
        {
            ColorTypeInput = InputType.Dec;
        }
        //protected override void OnPreviewKeyDown(System.Windows.Input.KeyEventArgs e)
        //{
        //    if (!(e.Key == Key.D0 || e.Key == Key.D5
        //        || e.Key == Key.D1 || e.Key == Key.D6
        //        || e.Key == Key.D2 || e.Key == Key.D7
        //        || e.Key == Key.D3 || e.Key == Key.D8
        //        || e.Key == Key.D4 || e.Key == Key.D9
        //        || e.Key == Key.Back || e.Key == Key.Delete
        //        ))
        //        e.Handled = true;

        //    base.OnPreviewKeyDown(e);
        //}

        //protected override void OnPreviewTextInput(System.Windows.Input.TextCompositionEventArgs e)
        //{
        //    e.Handled = "0123456789".IndexOf(e.Text) < 0;
        //    base.OnPreviewTextInput(e);
        //}
        protected override void OnPreviewTextInput(System.Windows.Input.TextCompositionEventArgs e)
        {
            if ("0123456789".IndexOf(e.Text) < 0)
            {
                e.Handled = true;
            }
            //else
            //{
            //    int t = Text
            //}
            base.OnPreviewTextInput(e);
        }

        //protected override void OnTextInput(TextCompositionEventArgs e)
        //{
        //    int x;
        //    if (!int.TryParse(this.Text + e.Text, out x))
        //    {
        //        e.Handled = true;
        //    }
        //    else
        //    {
        //        if ()
        //    }
        //    base.OnTextInput(e);
        //}

        //protected override void OnTextChanged(TextChangedEventArgs e)
        //{
        //    int x;
        //    if (!int.TryParse(Text, out x))
        //        e.
        //    base.OnTextChanged(e);
        //}
    }
}
