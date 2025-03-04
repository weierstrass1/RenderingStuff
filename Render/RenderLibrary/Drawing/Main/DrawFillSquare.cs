using ILGPU;
using ILGPU.Runtime;
using ILGPUUtils.ColorType;

namespace RenderLibrary.Drawing;

public partial class DrawFillSquare : IKernel
{
    public Accelerator Accelerator { get; private set; }
    public static IKernel CreateInstance(Accelerator accel)
    {
        return new DrawFillSquare(accel);
    }
    public DrawFillSquare(Accelerator accel)
    {
        kernel = accel
            .LoadAutoGroupedStreamKernel<Index2D, ArrayView<byte>, ARGBColor, Index2D, Index2D>
            (drawFillSquare);
        Accelerator = accel;
    }
    public void Run(MemoryBuffer1D<byte, Stride1D.Dense> dest, ARGBColor value, Index2D offset, Index2D size, Index2D dim)
    {
        Run(dest.View, value, offset, size, dim);
    }
    public void Run(ArrayView<byte> dest, ARGBColor value, Index2D offset, Index2D size, Index2D dim)
    {
        if (Accelerator == null)
            return;
        int w = Math.Min(dim.X - offset.X, size.X);
        int h = Math.Min(dim.Y - offset.Y, size.Y);
        Index2D area = (w, h);
        Accelerator.Synchronize();
        kernel(area, dest, value, offset, dim);
    }
}
