using ILGPU;
using ILGPU.Runtime;
using ILGPU.Runtime.CPU;
using ILGPU.Runtime.Cuda;
using ILGPU.Runtime.OpenCL;

namespace ILGPUUtils;

public class RenderCore : IDisposable
{
    public bool Disposed { get; private set; }
    public Context Context { get => _context!; }
    public Accelerator MainGPUAccelerator { get => _mainGPUAccelerator!; }
    public Accelerator MainCPUAccelerator { get => _mainCPUAccelerator!; }
    public Accelerator MainAccelerator { get => _mainAccelerator!; }
    private readonly List<MemoryBuffer> _buffers = [];
    private Context? _context;
    private Accelerator? _mainGPUAccelerator;
    private Accelerator? _mainCPUAccelerator;
    private Accelerator? _mainAccelerator;
    public AcceleratorType DefaultGPUType { get; private set; }
#if KERNEL_DEBUG
    private const AcceleratorType DefaultAcceleratorType = AcceleratorType.CPU;
#else
    private const AcceleratorType DefaultAcceleratorType = AcceleratorType.OpenCL;
#endif
    public RenderCore()
    {
        initialize();
    }
    private void initialize()
    {
        _context = Context.CreateDefault();
        _context.GetDevice<CudaDevice>(0);
        Device main = _context.GetDevice<CudaDevice>(0);
        DefaultGPUType = main != null ?
            AcceleratorType.Cuda :
            AcceleratorType.OpenCL;
        main ??= _context.GetDevice<CLDevice>(0);
        _mainGPUAccelerator = main.CreateAccelerator(_context);

        _mainCPUAccelerator = _context
                                .GetDevice<CPUDevice>(0)
                                .CreateAccelerator(_context);
        _mainAccelerator = DefaultAcceleratorType == AcceleratorType.CPU ?
            _mainCPUAccelerator :
            _mainGPUAccelerator;
        Disposed = false;
    }
    public MemoryBuffer1D<T, Stride1D.Dense> Allocate1D<T>(long lenght, AcceleratorType type = DefaultAcceleratorType) where T : unmanaged
    {
        if (Disposed)
            initialize();
        var newBuffer = type == AcceleratorType.CPU ?
                            MainCPUAccelerator.Allocate1D<T>(lenght) :
                            MainGPUAccelerator.Allocate1D<T>(lenght);
        _buffers.Add(newBuffer);
        return newBuffer;
    }
    public void DisposeBuffer(MemoryBuffer buffer)
    {
        _buffers.Remove(buffer);
        buffer.Dispose();
    }
    public void Dispose()
    {
        if (Disposed)
            return;
        foreach (var buffer in _buffers)
            buffer.Dispose();
        _buffers.Clear();
        MainGPUAccelerator.Dispose();
        MainCPUAccelerator.Dispose();
        Context.Dispose();
        Disposed = true;
        GC.SuppressFinalize(this);
    }
}