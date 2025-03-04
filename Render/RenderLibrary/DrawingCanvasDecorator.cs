using ILGPU;
using ILGPU.Runtime;
using ILGPUUtils;

namespace RenderLibrary;

public class DrawingCanvasDecorator : BaseDrawingCanvas
{
    public BaseDrawingCanvas Target { get; protected set; }
    public override Index2D Size { get => Target.Size; }
    public override MemoryBuffer1D<byte, Stride1D.Dense> Buffer { get => Target.Buffer; }
    public override RenderCore Core { get => Target.Core; }
    public override bool Disposed { get => Target.Disposed; }
    public DrawingCanvasDecorator(BaseDrawingCanvas target)
    {
        Target = target;
    }
    public override void Dispose()
    {
        Target.Dispose();
        GC.SuppressFinalize(this);
    }
}
