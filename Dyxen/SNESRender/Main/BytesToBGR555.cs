using ILGPU;
using ILGPU.Runtime;
using RenderLibrary.Drawing;
using SNESRender.ColorManagement;

namespace SNESRender
{
    public partial class BytesToBGR555 : IKernel
    {
        public Accelerator Accelerator { get; private set; }
        public static IKernel? CreateInstance(Accelerator accel)
        {
            return new BytesToBGR555(accel);
        }
        public BytesToBGR555(Accelerator accel)
        {
            Accelerator = accel;
            kernel = accel
                .LoadAutoGroupedStreamKernel<Index1D, ArrayView<byte>, ArrayView<BGR555Color>, int, int>
                (bytesToBGR555);
        }
        public void Run(MemoryBuffer1D<byte,Stride1D.Dense> src, MemoryBuffer1D<BGR555Color, Stride1D.Dense> dest,
            int srcOffset, int destOffset, int numberOfColors)
        {
            Run(src.View, dest.View, srcOffset, destOffset, numberOfColors);
        }
        public void Run(ArrayView<byte> src, ArrayView<BGR555Color> dest, int srcOffset, int destOffset, int numberOfColors)
        {
            if (Accelerator == null)
                return;
            Accelerator.Synchronize();
            kernel(numberOfColors, src, dest, srcOffset, destOffset);
        }
    }
}
