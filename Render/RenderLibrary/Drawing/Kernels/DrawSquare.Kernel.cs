using ILGPU;
using ILGPUUtils;
using ILGPUUtils.ColorType;

namespace RenderLibrary.Drawing;

public partial class DrawSquare : IKernel
{
    private readonly Action<Index1D, ArrayView<byte>, ARGBColor, Index2D, Index2D, Index2D> kernel;
    private static void drawSquare(Index1D index, ArrayView<byte> dest, ARGBColor value, Index2D Size, Index2D offset, Index2D dim)
    {
        int x = Math.Min(Size.X - 1, index);
        int y = Math.Min(Size.Y - 1, index);
        Index2D position = offset + dim - (1, 1);
        Index2D positionX0 = (offset.X + x, offset.Y);
        Index2D positionX1 = (offset.X + x, position.Y);
        Index2D positionY0 = (offset.X, offset.Y + y);
        Index2D positionY1 = (position.X, offset.Y + y);
        int i;

        if (positionX0.X >= 0 && positionX0.X < dim.X)
        {
            i = IndexUtils.Index2DToInt(positionX0, dim);
            IndexUtils.SetValue(i, dest, value);
        }
        if (positionX1.X >= 0 && positionX1.X < dim.X)
        {
            i = IndexUtils.Index2DToInt(positionX1, dim);
            IndexUtils.SetValue(i, dest, value);
        }
        if (positionY0.Y >= 0 && positionY0.Y < dim.Y)
        {
            i = IndexUtils.Index2DToInt(positionY0, dim);
            IndexUtils.SetValue(i, dest, value);
        }
        if (positionY1.Y >= 0 && positionY1.Y < dim.Y)
        {
            i = IndexUtils.Index2DToInt(positionY1, dim);
            IndexUtils.SetValue(i, dest, value);
        }
    }
}
