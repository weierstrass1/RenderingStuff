using ILGPU;
using ILGPUUtils;
using ILGPUUtils.ColorType;

namespace RenderLibrary.Drawing;

public partial class ChessBackground : IKernel
{
    private readonly Action<Index2D, ArrayView<byte>, ARGBColor, ARGBColor, Index2D, Index2D, Index2D> kernel;
    private static void chessBackground(Index2D index, ArrayView<byte> dest, ARGBColor value1, ARGBColor value2, Index2D offset, Index2D cellSize, Index2D dim)
    {
        Index2D position = index + offset;
        int x = Math.Abs(position.X) / cellSize.X;
        int y = Math.Abs(position.Y) / cellSize.Y;
        ARGBColor value = (x & 0x01) == (y & 0x01) ?
            value1 : 
            value2;
        int i = IndexUtils.Index2DToInt(index, dim);
        IndexUtils.SetValue(i, dest, value);
    }
}
