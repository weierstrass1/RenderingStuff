using ILGPU;
using ILGPU.Runtime;
using ILGPUUtils.ColorType;

namespace RenderLibrary.Drawing;

public partial class Grid : IKernel
{
    public Accelerator Accelerator { get; private set; }
    public static IKernel CreateInstance(Accelerator accel)
    {
        return new Grid(accel);
    }
    public Grid(Accelerator accel)
    {
        kernel = accel
            .LoadAutoGroupedStreamKernel<Index2D, ArrayView<byte>, ARGBColor, Index2D, Index2D, Index2D, Index2D, Index2D, Index2D>
            (grid);
        Accelerator = accel;
    }
    public void Run(ArrayView<byte> dest, ARGBColor value, Index2D lineLength, Index2D spacing, Index2D topLeftMargin, Index2D cellSize, Index2D offset, Index2D dim)
    {
        if (Accelerator == null)
            return;
        int x = Math.Max(1, cellSize.X);
        int y = Math.Max(1, cellSize.Y);
        Index2D cz = (x, y);
        Accelerator.Synchronize();
        kernel(dim, dest, value, lineLength, spacing, topLeftMargin, cz, offset, dim);
    }
}
