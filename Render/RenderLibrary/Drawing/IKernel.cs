using ILGPU.Runtime;

namespace RenderLibrary.Drawing
{
    public interface IKernel
    {
        public Accelerator Accelerator { get; }
        public static virtual IKernel? CreateInstance(Accelerator accel)
        {
            return default;
        }
    }
}
