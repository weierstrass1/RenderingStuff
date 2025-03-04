using ILGPU.Runtime;
using ILGPUUtils;

namespace RenderLibrary.Drawing;

public class KernelManager
{
    private static KernelManager? _instance;
    public static KernelManager Instance
    {
        get
        {
            _instance ??= new();
            return _instance;
        }
    }
    public static RenderCore Core { get => Instance._core; }
    private readonly Dictionary<Type, IKernel> _kernels;
    private readonly Accelerator _mainAccelerator;
    private readonly RenderCore _core;
    private KernelManager()
    {
        _core = new RenderCore();
        _mainAccelerator = _core.MainAccelerator;
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
        return Instance.getKernel<K>();
    }
}
