using EngineLibrary.Infrastructure.Enums.Font;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Client.Common.Native;
using SystemFontStyle = System.Drawing.FontStyle;

namespace Client.Common
{
    internal class TextRenderer
    {
        public FontType Font { get; set; } = FontType.Nice;
        private const int TextAlpha = 0;

        public string GetFontTypeName()
        {
            var name = Enum.GetName(Font);
            name ??= "Font is not have name";
            return name;
        }

        private Bitmap DefaultFont(TextNative text)
        {
            Font font;
            try
            {
                font = new Font(text.Font.Family, text.Font.Size * 1.25f, (SystemFontStyle)text.Font.Style);
            }
            catch
            {
                throw new Exception("Exception when trying to set System.Drawing.Font from TextNative.Text");
            }

            var size = MeasureTextSize(text.Text, font);
            var nextSize = new SizeF(HandleBitNumbers((uint)size.Width), HandleBitNumbers((uint)size.Height));
            if (nextSize.Width is 0 || nextSize.Height is 0)
                return new Bitmap(1, 1);

            var bmp = new Bitmap((int)nextSize.Width, (int)nextSize.Height);
            using var graphics = Graphics.FromImage(bmp);

            if (size.Width is not 0 && size.Height is not 0)
            {
                var format = StringFormat.GenericTypographic;

                graphics.FillRectangle(new SolidBrush(Color.FromArgb(TextAlpha, 0, 0, 0)), 0, 0, size.Width, size.Height);
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                var rectangle = new Rectangle { X = 0, Y = 0 };
                using (var path = GetStringPath(text.Text, rectangle, font, format))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    var offset = (RectangleF)rectangle;
                    offset.Offset(2, 2);
                    using (var offsetPath = GetStringPath(text.Text, offset, font, format))
                    {
                        var brush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
                        graphics.FillPath(brush, offsetPath);
                        brush.Dispose();
                    }

                    graphics.FillPath(new SolidBrush(Color.FromArgb(text.Color)), path);
                    graphics.DrawPath(Pens.Black, path);
                }
            }
            return bmp;
        }

        private GraphicsPath GetStringPath(string text, RectangleF rectangle, Font font, StringFormat format)
        {
            var path = new GraphicsPath();
            path.AddString(text, font.FontFamily, (int)font.Style, font.Size, rectangle, format);
            return path;
        }

        /// <summary>
        /// Measure text size with <see cref="System.Drawing.Font"/>
        /// </summary>
        /// <param name="text">The text to measure size</param>
        /// <param name="font">The font to measure size</param>
        /// <returns></returns>
        public SizeF MeasureTextSize(string text, Font font)
        {
            using var bmp = new Bitmap(1, 1);
            using var graphics = Graphics.FromImage(bmp);

            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            var tempFormat = StringFormat.GenericTypographic;

            tempFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

            return graphics.MeasureString(StripColorCodes(text), font, new PointF(0, 0), tempFormat);
        }

        /// <summary>
        /// Measure text size with <see cref="FontNative"/>
        /// </summary>
        /// <param name="text">The text to measure size</param>
        /// <param name="fontNative">The font to measure size</param>
        /// <returns></returns>
        public SizeF MeasureTextSize(string text, FontNative fontNative)
        {
            using var font = new Font(fontNative.Family, fontNative.Size, (SystemFontStyle)fontNative.Style);
            return MeasureTextSize(text, font);
        }

        /// <summary>
        /// Strips color codes in format '&x' from a given string.
        /// </summary>
        /// <param name="text">The text to process</param>
        /// <returns>The given text without any color codes</returns>
        public string StripColorCodes(string text)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < text.Length; i++)
            {
                if (text[i] is '&')
                {
                    if (i + 1 < text.Length && IsCharHex(text[i + 1]))
                        i++;
                    else
                        builder.Append(text[i]);
                }
                else
                    builder.Append(text[i]);
            }
            return builder.ToString();
        }

        protected uint HandleBitNumbers(uint bitNumber)
        {
            bitNumber--;
            bitNumber |= bitNumber >> 1;  //handle  2bit numbers
            bitNumber |= bitNumber >> 2;  //handle  4bit numbers
            bitNumber |= bitNumber >> 4;  //handle  8bit numbers
            bitNumber |= bitNumber >> 8;  //handle 16bit numbers
            bitNumber |= bitNumber >> 16; //handle 32bit numbers
            bitNumber++;
            return bitNumber;
        }

        protected bool IsCharHex(char ch)
        {
            return ch switch
            {
                >= '0' and <= '9' => true,
                >= 'a' and <= 'f' => true,
                >= 'A' and <= 'F' => true,
                _ => false
            };
        }
    }
}
