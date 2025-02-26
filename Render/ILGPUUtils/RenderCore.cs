using ILGPU;
using ILGPU.Runtime;
using ILGPU.Runtime.CPU;
using ILGPU.Runtime.Cuda;
using ILGPU.Runtime.OpenCL;

namespace ILGPUUtils;

public class RenderCore : IDisposable
{
    public bool Disposed { get; private set; }
    private readonly List<MemoryBuffer> buffers = [];
    public Context Context { get => context!; }
    public Accelerator MainGPUAccelerator { get => mainGPUAccelerator!; }
    public Accelerator MainCPUAccelerator { get => mainCPUAccelerator!; }
    private Context? context;
    private Accelerator? mainGPUAccelerator;
    private Accelerator? mainCPUAccelerator;
    public AcceleratorType DefaultGPUType { get; private set; }
    public RenderCore()
    {
        initialize();
    }
    private void initialize()
    {
        context = Context.CreateDefault();
        context.GetDevice<CudaDevice>(0);
        Device main = context.GetDevice<CudaDevice>(0);
        DefaultGPUType = main != null ?
            AcceleratorType.Cuda :
            AcceleratorType.OpenCL;
        main ??= context.GetDevice<CLDevice>(0);
        mainGPUAccelerator = main.CreateAccelerator(context);

        mainCPUAccelerator = context
                                .GetDevice<CPUDevice>(0)
                                .CreateAccelerator(context);
        Disposed = false;
    }
    public MemoryBuffer1D<T, Stride1D.Dense> Allocate1D<T>(long lenght) where T : unmanaged
    {
        return Allocate1D<T>(lenght, DefaultGPUType);
    }    
    public MemoryBuffer1D<T, Stride1D.Dense> Allocate1D<T>(long lenght, AcceleratorType type) where T : unmanaged
    {
        if (Disposed)
            initialize();
        var newBuffer = type == AcceleratorType.CPU ?
                            MainCPUAccelerator.Allocate1D<T>(lenght) :
                            MainGPUAccelerator.Allocate1D<T>(lenght);
        buffers.Add(newBuffer);
        return newBuffer;
    }
    public void DisposeBuffer(MemoryBuffer buffer)
    {
        buffers.Remove(buffer);
        buffer.Dispose();
    }
    public void Dispose()
    {
        if (Disposed)
            return;
        foreach (var buffer in buffers)
            buffer.Dispose();
        buffers.Clear();
        MainGPUAccelerator.Dispose();
        MainCPUAccelerator.Dispose();
        Context.Dispose();
        Disposed = true;
        GC.SuppressFinalize(this);
    }
}