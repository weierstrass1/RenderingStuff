using ILGPU;
using ILGPU.Runtime;

namespace RenderLibrary.Drawing;

public partial class ReSize : IKernel
{
    public Accelerator Accelerator { get; private set; }
    public ReSize(Accelerator accel)
    {
        kernel = accel
            .LoadAutoGroupedStreamKernel<Index2D, ArrayView<byte>, ArrayView<byte>, uint, Index2D, Index2D, Index2D>
                (reSize);
        Accelerator = accel;
    }
    public static IKernel CreateInstance(Accelerator accel)
    {
        return new ReSize(accel);
    }
    public void Run(MemoryBuffer1D<byte, Stride1D.Dense> src, MemoryBuffer1D<byte, Stride1D.Dense> dest, uint zoom, Index2D offset, Index2D dimSrc, Index2D dimDest)
    {
        Run(src.View, dest.View, zoom, offset, dimSrc, dimDest);
    }
    public void Run(ArrayView<byte> src, ArrayView<byte> dest, uint zoom, Index2D offset, Index2D dimSrc, Index2D dimDest)
    {
        if (Accelerator == null)
            return;
        Accelerator.Synchronize();
        kernel(dimDest, src, dest, zoom, offset, dimSrc, dimDest);
    }
}
