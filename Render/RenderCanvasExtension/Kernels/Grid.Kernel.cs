using ILGPU;
using ILGPUUtils;
using ILGPUUtils.ColorType;

namespace RenderLibrary.Drawing;

public partial class Grid : IKernel
{
    private readonly Action<Index2D, ArrayView<byte>, ARGBColor, Index2D, Index2D, Index2D, Index2D, Index2D, Index2D> kernel;
    private static void grid(Index2D index, ArrayView<byte> dest, ARGBColor value, Index2D lineLength, Index2D spacing, Index2D topLeftMargin, Index2D cellSize ,Index2D offset, Index2D dim)
    {
        Index2D position = index + offset;
        if (position.X < topLeftMargin.X || position.Y < topLeftMargin.Y)
            return;
        if (position.X % cellSize.X != cellSize.X - 1 && position.Y % cellSize.Y != cellSize.Y - 1)
            return;
        if ((position.X == dim.X - 1 && position.Y % cellSize.Y != cellSize.Y - 1) || 
            (position.Y == dim.Y - 1 && position.X % cellSize.X != cellSize.X - 1))
            return;
        Index2D lineRange = lineLength + spacing;
        if ((position.X % lineRange.X >= lineLength.X && position.Y % cellSize.Y == cellSize.Y - 1) || 
            (position.Y % lineRange.Y >= lineLength.Y && position.X % cellSize.X == cellSize.X - 1))
            return;
        int i = IndexUtils.Index2DToInt(index, dim);
        IndexUtils.SetValue(i, dest, value);
    }
}
