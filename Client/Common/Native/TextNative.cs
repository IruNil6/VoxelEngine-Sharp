using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineLibrary.Infrastructure.Enums.Font;

namespace Client.Common.Native
{
    public class TextNative
    {
        internal string Text { get; set; } = string.Empty;
        internal int Color { get; set; } = 0;
        internal FontNative Font { get; set; } = new FontNative();

        internal bool Equals(TextNative text)
        {
            return Text == text.Text
                   && Color == text.Color
                   && Font == text.Font;
        }
    }
}
