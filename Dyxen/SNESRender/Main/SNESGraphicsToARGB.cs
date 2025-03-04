using ILGPU;
using ILGPU.Runtime;
using RenderLibrary.Drawing;
using SNESRender.ColorManagement;

namespace SNESRender
{
    public partial class SNESGraphicsToARGB : IKernel
    {
        public Accelerator Accelerator { get; private set; }
        public static IKernel? CreateInstance(Accelerator accel)
        {
            return new SNESGraphicsToARGB(accel);
        }
        public SNESGraphicsToARGB(Accelerator accel)
        {
            Accelerator = accel;
            kernel = accel
                .LoadAutoGroupedStreamKernel<Index1D, ArrayView<byte>, ArrayView<BGR555Color>, ArrayView<byte>, int, int, int>
                (snesGraphicsToARGB);
        }
        public void Run(ArrayView<byte> src, ArrayView<BGR555Color> palette, ArrayView<byte> dest, int srcOffset, int palOffset, int destOffset, int numberOfColors)
        {
            if (Accelerator == null)
                return;

            Accelerator.Synchronize();
            kernel(numberOfColors, src, palette, dest, srcOffset, palOffset, destOffset);
        }
    }
}
