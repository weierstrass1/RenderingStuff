using ILGPU;
using ILGPU.Runtime;
using RenderLibrary.Drawing;
using SNESRender.Main;

namespace SNESRender
{
    public partial class LoadSNES4bppGraphics : IKernel, ILoadSNESGraphics
    {
        public int BPPs { get => 4; }
        public Accelerator Accelerator { get; private set; }
        public static IKernel? CreateInstance(Accelerator accel)
        {
            return new LoadSNES4bppGraphics(accel);
        }
        public LoadSNES4bppGraphics(Accelerator accel)
        {
            Accelerator = accel;
            kernel = accel
                .LoadAutoGroupedStreamKernel<Index1D, ArrayView<byte>, ArrayView<byte>, int, int>
                (loadSNES4bppGraphics);
        }
        public void Run(ArrayView<byte> src, ArrayView<byte> dest, int srcOffset, int destOffset)
        {
            if (Accelerator == null)
                return;
            Accelerator.Synchronize();
            kernel(src.IntExtent / 32, src, dest, srcOffset, destOffset);
        }
    }
}
