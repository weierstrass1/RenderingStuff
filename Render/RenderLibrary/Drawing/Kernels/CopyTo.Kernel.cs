using ILGPU;
using ILGPUUtils;

namespace RenderLibrary.Drawing;

public partial class CopyTo : IKernel
{
    private readonly Action<Index2D, ArrayView<byte>, ArrayView<byte>, Index2D, Index2D, Index2D, CopyToSkipInvisible> kernel;
    private static void copyTo(Index2D index, ArrayView<byte> src, ArrayView<byte> dest, Index2D offset, Index2D dimSrc, Index2D dimDest, CopyToSkipInvisible skipInvisible)
    {
        Index2D position = offset + index;
        if (position.X < 0 || position.X >= dimDest.X)
            return;
        if (position.Y < 0 || position.Y >= dimDest.Y)
            return;
        int j = IndexUtils.Index2DToInt(index, dimSrc);
        if (skipInvisible == CopyToSkipInvisible.Yes && src[j + 3] == 0)
            return;
        int i = IndexUtils.Index2DToInt(position, dimDest);
        IndexUtils.SetValue(i, dest, src[j], src[j + 1], src[j + 2], src[j + 3]);
    }
}
