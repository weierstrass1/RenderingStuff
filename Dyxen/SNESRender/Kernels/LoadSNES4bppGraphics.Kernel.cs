using ILGPU;

namespace SNESRender
{
    public partial class LoadSNES4bppGraphics
    {
        private static void loadSNES4bppGraphics(Index1D index, ArrayView<byte> src, ArrayView<byte> dest, int srcOffset, int destOffset, int srcDim, Index2D destDim)
        {
            int srcIndex = index * 4 + srcOffset;
            int destIndex = index
        }
    }
}
