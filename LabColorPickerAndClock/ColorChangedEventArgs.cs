using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LabColorPickerAndClock
{
    public class ColorChangedEventArgs : EventArgs
    {
        private Color _color;

        /// New color after change
        public Color Color { get => _color; }

        public ColorChangedEventArgs(Color newColor)
        {
            this._color = newColor;
        }

        private ColorChangedEventArgs() { }
    }
}
