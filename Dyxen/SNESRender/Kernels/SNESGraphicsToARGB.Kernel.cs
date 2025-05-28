using ILGPU;
using ILGPUUtils;
using ILGPUUtils.ColorType;
using SNESRender.ColorManagement;

namespace SNESRender
{
    public partial class SNESGraphicsToARGB
    {
        private Action<Index1D, ArrayView<byte>, ArrayView<BGR555Color>, ArrayView<byte>, int, int, int> kernel;
        private static void snesGraphicsToARGB(Index1D index, ArrayView<byte> src, ArrayView<BGR555Color> palette, ArrayView<byte> dest, int srcOffset, int palOffset, int destOffset)
        {
            Index1D destIndex = (index + destOffset) << 2;
            if (destIndex >= dest.Extent)
                return;
            Index1D srcIndex = (index + srcOffset);
            if (srcIndex >= src.Extent)
                return;
            int srcColor = src[srcIndex];
            if (srcColor == 0)
                return;
            Index1D palindex = palOffset + srcColor;
            if (palindex >= palette.Extent)
                return;
            ARGBColor value = palette[palindex];
            IndexUtils.SetValue(destIndex, dest, value);
        }
    }
}
