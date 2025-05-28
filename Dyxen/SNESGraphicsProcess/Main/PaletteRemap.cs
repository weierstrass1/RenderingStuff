using ILGPU;
using ILGPU.Runtime;
using RenderLibrary.Drawing;
using SNESRender.Main;

namespace SNESGraphicsProcess
{
    public partial class PaletteRemap<T> : IKernel where T : IKernel, ILoadSNESGraphics
    {
        public Accelerator Accelerator { get; private set; }
        public static IKernel? CreateInstance(Accelerator accel)
        {
            return new PaletteRemap<T>(accel);
        }
        public PaletteRemap(Accelerator accel)
        {
            Accelerator = accel;
            kernel = accel
                .LoadAutoGroupedStreamKernel<Index1D, ArrayView<byte>, ArrayView<byte>, int, byte>
                (paletteRemap);
        }
        public byte[] Run<K>(byte[] data, int offset, byte adder) where K : IKernel, ISaveSnesGraphics
        {
            T kernel = KernelManager.GetKernel<T>()!;
            K saveKernel = KernelManager.GetKernel<K>()!;
            int l = data.Length / 1024;
            l += data.Length % 1024 != 0 ? 1 : 0;
            l *= 1024;
            MemoryBuffer1D<byte, Stride1D.Dense> srcBuffer = KernelManager.Core.Allocate1D<byte>(l);
            MemoryBuffer1D<byte, Stride1D.Dense> destBuffer = KernelManager.Core.Allocate1D<byte>(l);
            srcBuffer.View.SubView(0, data.Length).CopyFromCPU(data);

            kernel.Run(srcBuffer.View, destBuffer.View, offset, offset);

            Run(destBuffer.View, srcBuffer.View, offset, adder);

            saveKernel.Run(srcBuffer.View, destBuffer.View, offset, offset);

            byte[] res = new byte[data.Length];

            destBuffer.View.SubView(0, data.Length).CopyToCPU(res);
            KernelManager.Core.DisposeBuffer(destBuffer);
            KernelManager.Core.DisposeBuffer(srcBuffer);
            return res;
        }
        public void Run(MemoryBuffer1D<byte, Stride1D.Dense> src, MemoryBuffer1D<byte, Stride1D.Dense> dest,
            int offset, byte adder)
        {
            Run(src.View, dest.View, offset, adder);
        }
        public void Run(ArrayView<byte> src, ArrayView<byte> dest, int offset, byte adder)
        {
            if (Accelerator == null)
                return;
            Accelerator.Synchronize();
            kernel((Index1D)(src.Extent - offset), src, dest, offset, adder);
        }
    }
}
