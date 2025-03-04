using ILGPUUtils.ColorType;
using System.Drawing;

namespace SNESRender.ColorManagement;

public struct BGR555Color
{
    public ushort Value;
    public byte HighByte { get => (byte)(Value >> 8); }
    public byte LowByte { get => (byte)(Value & 0x00FF); }
    public byte A
    {
        get => (Value & 0x8000) != 0 ?
            (byte)0 :
            (byte)255;
    }
    public byte R { get => (byte)((Value & 0x1F) << 3); }
    public byte G { get => (byte)(((Value >> 5) & 0x1F) << 3); }
    public byte B { get => (byte)(((Value >> 10) & 0x1F) << 3); }
    public BGR555Color(byte highByte, byte lowByte)
    {
        Value = (ushort)((highByte << 8) | (lowByte));
    }
    public BGR555Color(ushort value)
    {
        Value = value;
    }
    public BGR555Color(byte A, byte R, byte G, byte B)
    {
        if (R > 248)
            R = 248;
        if (G > 248)
            G = 248;
        if (B > 248)
            B = 248;
        Value = (ushort)((A > 127 ? 0 : 0x8000) |
            ((((B + 4) / 8) & 0x1F) << 10) |
            ((((G + 4) / 8) & 0x1F) << 5) |
            (((R + 4) / 8) & 0x1F));
    }
    public BGR555Color(byte R, byte G, byte B)
    {
        if (R > 248)
            R = 248;
        if (G > 248)
            G = 248;
        if (B > 248)
            B = 248;
        Value = (ushort)(((((B + 4) / 8) & 0x1F) << 10) |
            ((((G + 4) / 8) & 0x1F) << 5) |
            (((R + 4) / 8) & 0x1F));
    }
    public static implicit operator BGR555Color(Color c)
    {
        return new(c.A, c.R, c.G, c.B);
    }
    public static implicit operator Color(BGR555Color c)
    {
        return Color.FromArgb(c.A, c.R, c.G, c.B);
    }
    public static implicit operator BGR555Color(ARGBColor c)
    {
        return new(c.A, c.R, c.G, c.B);
    }
    public static implicit operator ARGBColor(BGR555Color c)
    {
        return new(c.A, c.R, c.G, c.B);
    }
    public override string ToString()
    {
        return $"{Value:X4}: A:{A:X2} - R:{R:X2} - G:{G:X2} - B:{B:X2}";
    }
}
