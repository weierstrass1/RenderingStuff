using ILGPU;
using ILGPU.Runtime;
using ILGPUUtils.ColorType;

namespace RenderLibrary.Drawing;

public partial class ChessBackground : IKernel
{
    public Accelerator Accelerator { get; private set; }
    public static IKernel CreateInstance(Accelerator accel)
    {
        return new ChessBackground(accel);
    }
    public ChessBackground(Accelerator accel)
    {
        kernel = accel
            .LoadAutoGroupedStreamKernel<Index2D, ArrayView<byte>, ARGBColor, ARGBColor, Index2D, Index2D, Index2D>
            (chessBackground);
        Accelerator = accel;
    }
    public void Run(ArrayView<byte> dest, ARGBColor value1, ARGBColor value2, Index2D offset, Index2D cellsize, Index2D dim)
    {
        if (Accelerator == null)
            return;
        int x = Math.Max(1, cellsize.X);
        int y = Math.Max(1, cellsize.Y);
        Index2D cz = (x, y);
        Accelerator.Synchronize();
        kernel(dim, dest, value1, value2, offset, cz, dim);
    }
}
