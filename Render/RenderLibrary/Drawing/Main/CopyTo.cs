using ILGPU;
using ILGPU.Runtime;

namespace RenderLibrary.Drawing;

public enum CopyToSkipInvisible { Yes, No }
public partial class CopyTo : IKernel
{
    public Accelerator Accelerator { get; private set; }
    public static IKernel CreateInstance(Accelerator accel)
    {
        return new CopyTo(accel);
    }
    public CopyTo(Accelerator accel)
    {
        kernel = accel
            .LoadAutoGroupedStreamKernel<Index2D, ArrayView<byte>, ArrayView<byte>, Index2D, Index2D, Index2D, CopyToSkipInvisible>
                (copyTo);
        Accelerator = accel;
    }
    public void Run(MemoryBuffer1D<byte, Stride1D.Dense> src, MemoryBuffer1D<byte, Stride1D.Dense> dest, Index2D offset, Index2D dimSrc, Index2D dimDest, CopyToSkipInvisible skipInvisible = CopyToSkipInvisible.No)
    {
        Run(src.View, dest.View, offset, dimSrc, dimDest, skipInvisible);
    }
    public void Run(ArrayView<byte> src, ArrayView<byte> dest, Index2D offset, Index2D dimSrc, Index2D dimDest, CopyToSkipInvisible skipInvisible = CopyToSkipInvisible.No)
    {
        if (Accelerator == null)
            return;
        int w = Math.Min(dimDest.X - offset.X, dimSrc.X);
        int h = Math.Min(dimDest.Y - offset.Y, dimSrc.Y);
        Index2D area = (w, h);
        Accelerator.Synchronize();
        kernel(area, src, dest, offset, dimSrc, dimDest, skipInvisible);
    }
}
