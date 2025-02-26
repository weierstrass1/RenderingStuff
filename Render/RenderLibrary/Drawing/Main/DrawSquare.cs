using ILGPU;
using ILGPU.Runtime;
using ILGPUUtils.ColorType;

namespace RenderLibrary.Drawing;

public partial class DrawSquare : IKernel
{
    public Accelerator Accelerator { get; private set; }
    public static IKernel CreateInstance(Accelerator accel)
    {
        return new DrawSquare(accel);
    }
    public DrawSquare(Accelerator accel)
    {
        kernel = accel.LoadAutoGroupedStreamKernel<Index1D, ArrayView<byte>, ARGBColor, Index2D, Index2D, Index2D>
            (drawSquare);
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
        Index1D index = new(Math.Max(size.X, size.Y));
        Accelerator.Synchronize();
        kernel(index, dest, value, size, offset, dim);
    }
}
