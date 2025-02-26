using ILGPU.Runtime;

namespace RenderLibrary.Drawing;

public static class KernelFactory
{
    public static T? CreateInstance<T>(Accelerator accel) where T : IKernel
    {
        IKernel? k = T.CreateInstance(accel);
        if (k == null)
            return default;
        return (T)k;
    }
}
