using ILGPU;
using ILGPU.Runtime;
using ILGPUUtils;
using RenderLibrary.Drawing;

namespace RenderLibrary;

public class DrawingCanvas : BaseDrawingCanvas
{
    private Index2D size;
    public override Index2D Size { get => size; }
    public override MemoryBuffer1D<byte, Stride1D.Dense> Buffer { get; }
    public override RenderCore Core { get;}
    public override bool Disposed { get => _disposed; }
    private bool _disposed;
    public DrawingCanvas(int width, int height)
    {
        Core = KernelManager.Core;
        size = (width, height);
        Buffer = Core.Allocate1D<byte>(width * height * 4);
        _disposed = false;
    }
    public DrawingCanvas(int width, int height, RenderCore core)
    {
        Core = core;
        size = (width, height);
        Buffer = Core.Allocate1D<byte>(width * height * 4);
        _disposed = false;
    }
    public override void Dispose()
    {
        if (_disposed)
            return;
        _disposed = true;
        Core.DisposeBuffer(Buffer);
        GC.SuppressFinalize(this);
    }
}
