using ILGPU;
using ILGPU.Runtime;
using RenderLibrary.Drawing;
using SNESRender.ColorManagement;

namespace SNESRender
{
    public partial class BGR555ToARGB : IKernel
    {
        public Accelerator Accelerator { get; private set; }
        public static IKernel? CreateInstance(Accelerator accel)
        {
            return new BGR555ToARGB(accel);
        }
        public BGR555ToARGB(Accelerator accel)
        {
            Accelerator = accel;
            kernel = accel
                .LoadAutoGroupedStreamKernel<Index1D, ArrayView<BGR555Color>, ArrayView<byte>, int, int>
                (bgr555ToARGB);
        }
        public void Run(MemoryBuffer1D<BGR555Color, Stride1D.Dense> src, MemoryBuffer1D<byte, Stride1D.Dense> dest,
            int srcOffset, int destOffset, int numberOfColors)
        {
            Run(src.View, dest.View, srcOffset, destOffset, numberOfColors);
        }
        public void Run(ArrayView<BGR555Color> src, ArrayView<byte> dest, int srcOffset, int destOffset, int numberOfColors)
        {
            if (Accelerator == null)
                return;
            Accelerator.Synchronize();
            kernel(numberOfColors, src, dest, srcOffset, destOffset);
        }
    }
}
