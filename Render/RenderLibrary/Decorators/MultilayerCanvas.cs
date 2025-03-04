namespace RenderLibrary.Decorators;

public class MultilayerCanvas : DrawingCanvasDecorator
{
    private readonly List<BaseDrawingCanvas> layers;
    public MultilayerCanvas(int width, int height) : base(new DrawingCanvas(width, height))
    {
        layers = [];
    }
    public void AddLayer(BaseDrawingCanvas layer)
    {
        layers.Add(layer);
        Draw(layer, (0, 0));
        layer.Changed += canvas => refresh();
        TriggerChangedEvent();
    }
    public void RemoveLayer(BaseDrawingCanvas layer)
    {
        layers.Remove(layer);
        layer.Changed -= canvas => refresh();
        refresh();
    }
    public void refresh()
    {
        Clear();
        foreach (var layer in layers)
        {
            Draw(layer, (0, 0));
        }
        TriggerChangedEvent();
    }
}
