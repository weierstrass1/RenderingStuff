using ILGPU.Runtime;
using RenderLibrary.Drawing;

namespace SNESRender
{
    public partial class LoadSNES4bppGraphics : IKernel
    {
        public Accelerator Accelerator { get; private set; }
        public static IKernel? CreateInstance(Accelerator accel)
        {
            return new LoadSNES4bppGraphics(accel);
        }
        public LoadSNES4bppGraphics(Accelerator accel)
        {
            Accelerator = accel;
        }
    }
}
