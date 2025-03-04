using ILGPU;
using SNESRender.ColorManagement;

namespace SNESRender
{
    public partial class BytesToBGR555
    {
        private Action<Index1D, ArrayView<byte>, ArrayView<BGR555Color>, int, int> kernel;
        private static void bytesToBGR555(Index1D index, ArrayView<byte> src, ArrayView<BGR555Color> dest, int srcOffset, int destOffset)
        {
            int srcIndex = index * 2;
            if (srcIndex + 1 >= src.Extent || index >= dest.Extent)
                return;
            dest[index] = new(src[srcIndex + 1], src[srcIndex]);
        }
    }
}
