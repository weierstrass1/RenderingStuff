using ILGPU;
using RenderLibrary.Drawing;

namespace RenderLibrary.Decorators;

public class ZoomeableDrawingCanvas : DrawingCanvasDecorator
{
    public uint _zoom = 1;
    public uint Zoom
    {
        get => _zoom;
        set
        {
            if (value == 0 || _zoom == value)
                return;
            _zoom = value;
            Refresh();
        }
    }
    public Index2D Offset;
    public BaseDrawingCanvas MainCanvas { get; private set; }
    public event Action<Index2D>? SizeChanged;
    public ZoomeableDrawingCanvas(int width, int height, BaseDrawingCanvas mainCanvas) : base(new DrawingCanvas(width, height))
    {
        MainCanvas = mainCanvas;
        MainCanvas.Changed += canvas => Refresh();
        Offset = (0, 0);
    }
    public void ChangeSize(int width, int height)
    {
        if (width == Width && height == Height)
            return;
        Target.Dispose();
        Target = new DrawingCanvas(width, height);
        Refresh();
        SizeChanged?.Invoke((width, height));
    }
    public void Refresh()
    {
        Clear();
        ReSize resize = KernelManager.GetKernel<ReSize>()!;
        resize.Run(MainCanvas.Buffer, Buffer, Zoom, Offset, MainCanvas.Size, Size);
        TriggerChangedEvent();
    }
    public override void Dispose()
    {
        MainCanvas.Changed -= canvas => Refresh();
        base.Dispose();
        GC.SuppressFinalize(this);
    }

    public EventHandler ChangeSize(object width, object height)
    {
        throw new NotImplementedException();
    }
}
