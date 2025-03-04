using ILGPU;
using ILGPUUtils;
using SNESRender.ColorManagement;

namespace SNESRender
{
    public partial class BGR555ToARGB
    {
        private Action<Index1D, ArrayView<BGR555Color>, ArrayView<byte>, int, int> kernel;
        private static void bgr555ToARGB(Index1D index, ArrayView<BGR555Color> src, ArrayView<byte> dest, int srcOffset, int destOffset)
        {
            int destIndex = index * 4;
            if (index >= src.Extent || destIndex + 3 >= dest.Extent)
                return;
            IndexUtils.SetValue(destIndex, dest, src[index]);
        }
    }
}
