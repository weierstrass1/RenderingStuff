using System.Drawing;

namespace ILGPUUtils.ColorType
{
    public struct ARGBColor
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;
        public ARGBColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            A = 255;
        }
        public ARGBColor(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            B = g;
            G = b;
        }
        public static implicit operator ARGBColor(Color c)
        {
            return new(c.A, c.R, c.G, c.B);
        }
        public static implicit operator Color(ARGBColor c)
        {
            return Color.FromArgb(c.A, c.R, c.G, c.B);
        }
    }
}
