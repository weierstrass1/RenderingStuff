using ILGPU;
using ILGPUUtils;
using ILGPUUtils.ColorType;

namespace RenderLibrary.Drawing;

public partial class DrawFillSquare : IKernel
{
    private readonly Action<Index2D, ArrayView<byte>, ARGBColor, Index2D, Index2D> kernel;
    private static void drawFillSquare(Index2D index, ArrayView<byte> dest, ARGBColor value, Index2D offset, Index2D dim)
    {
        Index2D position = index + offset;
        if (position.X < 0 || position.X >= dim.X)
            return;
        if (position.Y < 0 || position.Y >= dim.Y)
            return;

        int i = IndexUtils.Index2DToInt(position, dim);
        IndexUtils.SetValue(i, dest, value);
    }
}
