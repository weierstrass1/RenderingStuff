using ILGPU;
using SNESRender.ColorManagement;

namespace SNESRender
{
    public partial class BGR555ToPal
    {
        private Action<Index1D, ArrayView<BGR555Color>, ArrayView<byte>, int, int> kernel;
        private static void bgr555ToPal(Index1D index, ArrayView<BGR555Color> src, ArrayView<byte> dest, int srcOffset, int destOffset)
        {
            int destIndex = index * 3;
            if (index >= src.Extent || destIndex + 2 >= dest.Extent)
                return;
            dest[destIndex] = src[index].R;
            dest[destIndex + 1] = src[index].G;
            dest[destIndex + 2] = src[index].B;
        }
    }
}
