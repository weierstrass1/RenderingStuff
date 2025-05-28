using ILGPU;
using ILGPU.Runtime;
using RenderLibrary.Drawing;
using SNESRender.Main;

namespace SNESRender
{
    public partial class LoadSNES8bppGraphics : IKernel, ILoadSNESGraphics
    {
        public static int BPPs { get => 8; }
        public Accelerator Accelerator { get; private set; }
        public static IKernel? CreateInstance(Accelerator accel)
        {
            return new LoadSNES8bppGraphics(accel);
        }
        public LoadSNES8bppGraphics(Accelerator accel)
        {
            Accelerator = accel;
            kernel = accel
                .LoadAutoGroupedStreamKernel<Index1D, ArrayView<byte>, ArrayView<byte>, int, int>
                (loadSNES8bppGraphics);
        }
        public void Run(ArrayView<byte> src, ArrayView<byte> dest, int srcOffset, int destOffset)
        {
            if (Accelerator == null)
                return;
            Accelerator.Synchronize();
            kernel(src.IntExtent / 64, src, dest, srcOffset, destOffset);
        }
    }
}
