using ILGPU;
using RenderLibrary.Drawing;
using SNESRender.Main;

namespace SNESGraphicsProcess
{
    public partial class PaletteRemap<T> : IKernel where T : IKernel, ILoadSNESGraphics
    {
        private Action<Index1D, ArrayView<byte>, ArrayView<byte>, int, byte> kernel;
        private static void paletteRemap(Index1D index, ArrayView<byte> src, ArrayView<byte> dest, int offset, byte adder)
        {
            if (src[index] <= offset)
            {
                dest[index] = src[index];
                return;
            }
            byte val = (byte)(src[index] + adder);
            dest[index] = val;
        }
    }
}
