using ILGPU;
using ILGPUUtils;
using ILGPUUtils.ColorType;

namespace RenderLibrary.Drawing;

public partial class ReSize : IKernel
{
    private readonly Action<Index2D, ArrayView<byte>, ArrayView<byte>, uint, Index2D, Index2D, Index2D> kernel;
    private static void reSize(Index2D index, ArrayView<byte> src, ArrayView<byte> dest, uint zoom, Index2D offset, Index2D dimSrc, Index2D dimDest)
    {
        Index2D positionSrc = index + offset;
        positionSrc = new((int)(positionSrc.X / zoom), (int)(positionSrc.Y / zoom));
        if (positionSrc.X < 0 || positionSrc.X >= dimSrc.X)
            return;
        if (positionSrc.Y < 0 || positionSrc.Y >= dimSrc.Y)
            return;
        int i = IndexUtils.Index2DToInt(index, dimDest);
        int j = IndexUtils.Index2DToInt(positionSrc, dimSrc);
        ARGBColor value = new(src[j + 3], src[j + 2], src[j + 1], src[j]);
        IndexUtils.SetValue(i, dest, value);
    }
}
