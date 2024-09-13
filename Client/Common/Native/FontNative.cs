using EngineLibrary.Infrastructure.Enums.Font;

namespace Client.Common.Native
{
    internal class FontNative
    {
        public string Family { get; } = "Arial";
        public float Size { get; } = 12;
        public FontStyle Style { get; } = FontStyle.Regular;

        /// <summary>
        /// Initializes a new instance of the <see cref="FontNative"/> with default params.
        /// </summary>
        public FontNative() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FontNative"/> class with(out) the specified font family or/and size or/and style.
        /// </summary>
        /// <param name="family">The font family</param>
        /// <param name="size">The font size</param>
        /// <param name="style">The font style available only from FontStyle enum</param>
        public FontNative(string? family, float? size, FontStyle? style)
        {
            Family = family ?? Family;
            Size = size ?? Size;
            Style = style ?? Style;
        }
    }
}
