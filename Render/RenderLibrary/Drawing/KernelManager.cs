using ILGPU.Runtime;
using ILGPUUtils;

namespace RenderLibrary.Drawing;

public class KernelManager
{
    private static KernelManager? _instance;
    private static KernelManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new();
            return _instance;
        }
    }
    public static RenderCore Core { get => instance._core; }
    private readonly Dictionary<Type, IKernel> _kernels;
    private Accelerator _mainAccelerator;
    private RenderCore _core;
    private KernelManager()
    {
        _core = new RenderCore();
        _mainAccelerator = _core.MainGPUAccelerator;
        _kernels = [];
    }
    private void addKernel<K>() where K : IKernel
    {
        IKernel? kernel = KernelFactory.CreateInstance<K>(_mainAccelerator);
        if (kernel != null)
            _kernels.Add(typeof(K), kernel);
    }
    private K? getKernel<K>() where K : IKernel
    {
        IKernel? k;
        if (_kernels.TryGetValue(typeof(K), out k))
            return (K)k;
        addKernel<K>();
        if (!_kernels.ContainsKey(typeof(K)))
            return default;
        return (K)_kernels[typeof(K)];
    }
    public static K? GetKernel<K>() where K : IKernel
    {
        return instance.getKernel<K>();
    }
}
